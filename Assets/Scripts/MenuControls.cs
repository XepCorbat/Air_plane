using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    
    public void BackPressed()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Summer map loaded!");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed");
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

    public void time()
    {
        Time.timeScale = 1f;
    }
}
