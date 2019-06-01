using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class windisplay : MonoBehaviour
{
    public GameObject gameOverUI, p1, p2;


    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        if (win() == "blue")
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Winner of these 3 games is\n" + "Blue" + " !"+"\r\n ( "+ Challenge.geti(1) + " - " + Challenge.geti(2)+" )";
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().color = p1.GetComponent<MolePlayer>().GetColor();
        }
        else if (win() == "red")
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Winner of these 3 games is\n" + "Red" + " !" + "\r\n ( " + Challenge.geti(2) + " - " + Challenge.geti(1) + " )";
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().color = p2.GetComponent<MolePlayer>().GetColor();

        }
        else
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Equality !";
        }
    }

    public string win()
    {
        {
            if (Challenge.geti(1) > Challenge.geti(2))
                return "blue";
            else if (Challenge.geti(1) < Challenge.geti(2))
                return "red";
            else
                return "Equality";
        }
    }
}
