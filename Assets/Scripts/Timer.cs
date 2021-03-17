using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 20f;
    public GameObject EndGamePanel;
    public bool play = false;
    public Text TimerText;
    public Image endGameImage;
    void Update()
    {
        if (play)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                Debug.Log(timer);
                AudioListener.pause = false;        
                TimerText.text = "Time " + System.Convert.ToInt32(timer).ToString(); 
            }
            else
            {
                EndGamePanel.SetActive(true);
                endGameImage.enabled = true;
                AudioListener.pause = true;
                Time.timeScale = 0f;
            }
        }
    }
    public void playButton()
    {
        play = true;
    }
}
