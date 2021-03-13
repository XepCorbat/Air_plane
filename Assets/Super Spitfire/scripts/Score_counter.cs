using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_counter : MonoBehaviour
{
    public Text ScoreText;
    int Score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "circle")
        {
            Score++;
            ScoreText.text = Score.ToString();
            Destroy(other.gameObject);
        }
    }
}
