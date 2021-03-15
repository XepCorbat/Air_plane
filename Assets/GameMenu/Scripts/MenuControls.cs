using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("WinterMap");
    }

    public void BackPressed()
    {
        Application.Quit();
        SceneManager.LoadScene("MainMenu");
    }

    public void SummerMapPressed()
    {
        SceneManager.LoadScene("SummerMap");
        Debug.Log("Summer map loaded!");
    }

    public void WinterMapPressed()
    {
        SceneManager.LoadScene("WinterMap");
        Debug.Log("Winter map loaded!");
    }
}
