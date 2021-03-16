using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timer = 0f;
    public GameObject EndGamePanel;
    public bool play = false;
    void Update()
    {
        if (play)
        {
            if (timer < 20f)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);

            }
            else
            {
                EndGamePanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    public void playButton()
    {
        play = true;
    }
}
