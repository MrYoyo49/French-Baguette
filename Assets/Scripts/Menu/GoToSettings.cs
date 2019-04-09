using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToSettings : MonoBehaviour
{

    public void GoSettings(string strname)
    {
        SceneManager.LoadScene(strname);
    }
    
}
