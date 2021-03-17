using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCcontrol : MonoBehaviour
{
    public GameObject ButtonPlay;
    public GameObject ButtonBack;
    public Animator proppeler;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))        
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
                ButtonPlay.SetActive(true);
                ButtonBack.SetActive(true);
                AudioListener.pause = true;
            }
            else
            {
                Time.timeScale = 1f;
                ButtonPlay.SetActive(false);
                ButtonBack.SetActive(false);
                AudioListener.pause = false;
            }
        }
    }
}
