using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCcontrol : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
        {
            if (Time.timeScale == 1f)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f
        }
    }
}
