using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCcontrol : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;

    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            ispuse = true;
            AudioListener.pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            ispuse = false;
            AudioListener.pause = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

        }
    }
    //public void OnGUI()
    //{
    //    if (guipuse == true)
    //    {
    //        Cursor.visible = true;// включаем отображение курсора
    //        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f), "Продолжить"))
    //        {
    //            ispuse = false;
    //            timer = 0;
    //            Cursor.visible = false;
    //            AudioListener.pause = false;
    //        }
    //        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "Сохранить"))
    //        {

    //        }
    //        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "Загрузить"))
    //        {

    //        }
    //        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "В Меню"))
    //        {
    //            SceneManager.LoadScene("MainMenu");
    //            ispuse = false;
    //            timer = 0;
    //        }
    //    }
    //}
}
