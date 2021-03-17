using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.HasKey("save_score"))
        {
            gameObject.GetComponent<Text>().text += " " + PlayerPrefs.GetInt("save_score").ToString();
        }
        else
        {
            gameObject.GetComponent<Text>().text += " 0";
        }
    }
}
