using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    [RequireComponent(typeof(Rigidbody))]
    public class AeroplaneController : MonoBehaviour
    {
        public float maxRollAngle = 80;
        public float maxPitchAngle = 80;
        public Animator elerons;
        public Animator ruli;

        [SerializeField] private float m_MaxEnginePower = 40f;
        [SerializeField] private float m_Lift = 0.002f;
        [SerializeField] private float m_ZeroLiftSpeed = 300;
        [SerializeField] private float m_RollEffect = 1f;
        [SerializeField] private float m_PitchEffect = 1f;
        [SerializeField] private float m_YawEffect = 0.2f;
        [SerializeField] private float m_BankedTurnEffect = 0.5f;
        [SerializeField] private float m_AerodynamicEffect = 0.02f;
        [SerializeField] private float m_AutoTurnPitch = 0.5f;
        [SerializeField] private float m_AutoRollLevel = 0.2f;
        [SerializeField] private float m_AutoPitchLevel = 0.2f;
        [SerializeField] private float m_AirBrakesEffect = 3f;
        [SerializeField] private float m_ThrottleChangeSpeed = 0.3f;
        [SerializeField] private float m_DragIncreaseFactor = 0.001f;

        //public float Altitude { get; private set; }
        public float Throttle { get; private set; }
        public bool AirBrakes { get; private set; }
        public float ForwardSpeed { get; private set; }
        public float EnginePower { get; private set; }
        public float MaxEnginePower { get { return m_MaxEnginePower; } }
        public float RollAngle { get; private set; }
        public float PitchAngle { get; private set; }
        public float RollInput { get; private set; }
        public float PitchInput { get; private set; }
        public float YawInput { get; private set; }
        public float ThrottleInput { get; private set; }

        private float m_OriginalDrag;
        private float m_OriginalAngularDrag;
        private float m_AeroFactor;
        private float m_BankedTurnAmount;
        private Rigidbody m_Rigidbody;


        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_OriginalDrag = m_Rigidbody.drag;
            m_OriginalAngularDrag = m_Rigidbody.angularDrag;
            for (int i = 0; i < transform.childCount; i++)
            {
                foreach (var componentsInChild in transform.GetChild(i).GetComponentsInChildren<WheelCollider>())
                {
                    componentsInChild.motorTorque = 0.18f;
                }
            }
        }

        private void FixedUpdate()
        {
            float roll = Input.GetAxis("Horizontal");
            if (roll > 0)
            {
                if (elerons.GetBool("right_down"))
                {
                    elerons.SetBool("right_down", false);
                }
                elerons.SetBool("down_right", true);
            }
            else if (roll < 0)
            {
                if (elerons.GetBool("down_right"))
                {
                    elerons.SetBool("down_right", false);
                }
                elerons.SetBool("right_down", true);
            }
            else
            {
                elerons.SetBool("down_right", false);
                elerons.SetBool("right_down", false);
            }
            float pitch = Input.GetAxis("Vertical");
            if (pitch < 0)
            {
                if (ruli.GetBool("down"))
                {
                    ruli.SetBool("down", false);
                }
                ruli.SetBool("up", true);
            }
            else if (pitch > 0)
            {
                if (ruli.GetBool("up"))
                {
                    ruli.SetBool("up", false);
                }
                ruli.SetBool("down", true);
            }
            else
            {
                ruli.SetBool("up", false);
                ruli.SetBool("down", false);
            }
            bool airBrakes = Input.GetButton("Fire1");
            float throttle = airBrakes ? -1 : 1;
            Move(roll, pitch, 0, throttle, airBrakes);
        }

        void Move(float rollInput, float pitchInput, float yawInput, float throttleInput, bool airBrakes)
        {
            RollInput = rollInput;
            PitchInput = pitchInput;
            YawInput = yawInput;
            ThrottleInput = throttleInput;
            AirBrakes = airBrakes;
            ClampInputs();
            CalculateRollAndPitchAngles();
            AutoLevel();
            CalculateForwardSpeed();
            ControlThrottle();
            CalculateDrag();
            CaluclateAerodynamicEffect();
            CalculateLinearForces();
            CalculateTorque();
            //CalculateAltitude();
        }

        void ClampInputs()
        {
            RollInput = Mathf.Clamp(RollInput, -1, 1);
            PitchInput = Mathf.Clamp(PitchInput, -1, 1);
            YawInput = Mathf.Clamp(YawInput, -1, 1);
            ThrottleInput = Mathf.Clamp(ThrottleInput, -1, 1);
        }

        void CalculateRollAndPitchAngles()
        {
            var flatForward = transform.forward;
            flatForward.y = 0;
            if (flatForward.sqrMagnitude > 0)
            {
                flatForward.Normalize();
                var localFlatForward = transform.InverseTransformDirection(flatForward);
                PitchAngle = Mathf.Atan2(localFlatForward.y, localFlatForward.z);
                var flatRight = Vector3.Cross(Vector3.up, flatForward);
                var localFlatRight = transform.InverseTransformDirection(flatRight);
                RollAngle = Mathf.Atan2(localFlatRight.y, localFlatRight.x);
            }
        }

        void AutoLevel()
        {
            m_BankedTurnAmount = Mathf.Sin(RollAngle);
            if (RollInput == 0f)
            {
                RollInput = -RollAngle * m_AutoRollLevel;
            }
            if (PitchInput == 0f)
            {
                PitchInput = -PitchAngle * m_AutoPitchLevel;
                PitchInput -= Mathf.Abs(m_BankedTurnAmount * m_BankedTurnAmount * m_AutoTurnPitch);
            }
        }

        void CalculateForwardSpeed()
        {
            var localVelocity = transform.InverseTransformDirection(m_Rigidbody.velocity);
            ForwardSpeed = Mathf.Max(0, localVelocity.z);
        }

        void ControlThrottle()
        {
            Throttle = Mathf.Clamp01(Throttle + ThrottleInput * Time.deltaTime * m_ThrottleChangeSpeed);
            EnginePower = Throttle * m_MaxEnginePower;
        }

        void CalculateDrag()
        {
            float extraDrag = m_Rigidbody.velocity.magnitude * m_DragIncreaseFactor;
            m_Rigidbody.drag = (AirBrakes ? (m_OriginalDrag + extraDrag) * m_AirBrakesEffect : m_OriginalDrag + extraDrag);
            m_Rigidbody.angularDrag = m_OriginalAngularDrag * ForwardSpeed;
        }

        void CaluclateAerodynamicEffect()
        {
            if (m_Rigidbody.velocity.magnitude > 0)
            {
                m_AeroFactor = Vector3.Dot(transform.forward, m_Rigidbody.velocity.normalized);
                m_AeroFactor *= m_AeroFactor;
                var newVelocity = Vector3.Lerp(m_Rigidbody.velocity, transform.forward * ForwardSpeed,
                                               m_AeroFactor * ForwardSpeed * m_AerodynamicEffect * Time.deltaTime);
                m_Rigidbody.velocity = newVelocity;
                m_Rigidbody.rotation = Quaternion.Slerp(m_Rigidbody.rotation,
                                                      Quaternion.LookRotation(m_Rigidbody.velocity, transform.up),
                                                      m_AerodynamicEffect * Time.deltaTime);
            }
        }

        void CalculateLinearForces()
        {
            var forces = Vector3.zero;
            forces += EnginePower * transform.forward;
            var liftDirection = Vector3.Cross(m_Rigidbody.velocity, transform.right).normalized;
            var zeroLiftFactor = Mathf.InverseLerp(m_ZeroLiftSpeed, 0, ForwardSpeed);
            var liftPower = ForwardSpeed * ForwardSpeed * m_Lift * zeroLiftFactor * m_AeroFactor;
            forces += liftPower * liftDirection;
            m_Rigidbody.AddForce(forces);
        }

        void CalculateTorque()
        {
            var torque = Vector3.zero;
            torque += PitchInput * m_PitchEffect * transform.right;
            torque += YawInput * m_YawEffect * transform.up;
            torque += -RollInput * m_RollEffect * transform.forward;
            torque += m_BankedTurnAmount * m_BankedTurnEffect * transform.up;
            m_Rigidbody.AddTorque(torque * ForwardSpeed * m_AeroFactor);
        }

        //void CalculateAltitude()
        //{
        //    var ray = new Ray(transform.position - Vector3.up * 10, -Vector3.up);
        //    RaycastHit hit;
        //    Altitude = Physics.Raycast(ray, out hit) ? hit.distance + 10 : transform.position.y;
        //}
    }
}
