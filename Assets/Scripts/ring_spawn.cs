using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
  спавн колец:

         public class ring_spawn : MonoBehaviour
        {
            public GameObject nextRing;

            void Start()
            {
                Application.targetFrameRate = 60; 
            }
            private void OnTriggerEnter(Collider other)
            {
                nextRing.SetActive(true);
            }
        }

 */
public class ring_spawn : MonoBehaviour
{
    public GameObject nextRing;

    void Start()
    {
        Application.targetFrameRate = 60; 
    }
    private void OnTriggerEnter(Collider other)
    {
        nextRing.SetActive(true);
    }
}
