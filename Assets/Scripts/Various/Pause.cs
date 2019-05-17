using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject menuPanelUI;


    public void PauseGame()
    {   
        if (!WhackAMoleController.GameIsEnded)
        {
            Time.timeScale = 0;
            menuPanelUI.SetActive(true);
        }

    }
    public void  Resume()
    {
        Time.timeScale = 1;
        menuPanelUI.SetActive(false);
    }
    public void ExiToScene(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }
}
