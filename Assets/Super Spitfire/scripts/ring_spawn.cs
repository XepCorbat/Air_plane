using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring_spawn : MonoBehaviour
{
    public GameObject nextRing;
    private void OnTriggerEnter(Collider other)
    {
        nextRing.SetActive(true);
    }
}
