using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour
{
    public string key;
    void Start()
    {
        if (PlayerPrefs.HasKey(key))
        {
            gameObject.GetComponent<Text>().text += " " + PlayerPrefs.GetInt(key).ToString();
        }
        else
        {
            gameObject.GetComponent<Text>().text += " 0";
        }
    }
}
