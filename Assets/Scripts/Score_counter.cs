using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//вывод и сохранение счета
public class Score_counter : MonoBehaviour
{
    public Text ScoreText;
    public Text TextEnd;
    int Score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "circle")
        {
            Score++;
            ScoreText.text = "Score " + Score.ToString();
            TextEnd.text = "Your result " + Score.ToString();
            Debug.Log(SceneManager.GetActiveScene().name);
            string Scene = SceneManager.GetActiveScene().name;
            if(Scene == "WinterMap")
            {
                if (PlayerPrefs.HasKey("save_score"))
                {
                    int saveScore = PlayerPrefs.GetInt("save_score");
                    if (saveScore < Score)
                    {
                        PlayerPrefs.SetInt("save_score", Score);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("save_score", Score);
                }
            }
            else if (Scene == "SummerMap")
            {
                if (PlayerPrefs.HasKey("save_score_summer"))
                {
                    int saveScore = PlayerPrefs.GetInt("save_score_summer");
                    if (saveScore < Score)
                    {
                        PlayerPrefs.SetInt("save_score_summer", Score);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("save_score_summer", Score);
                }
            }
            Destroy(other.gameObject);            
        }

    }
}
