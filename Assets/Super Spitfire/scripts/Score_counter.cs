using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Destroy(other.gameObject);
            
        }

    }
}
