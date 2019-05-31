using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class HockeyController : MonoBehaviour
{
    float initialTime = 60, remainingTime;
    static bool GameIsEnded = false;
    public GameObject player1, player2;
    public Sprite player1moleSprite, player2moleSprite;
    public MolePlayer playerObject;
    public GameObject TextGO1, TextGO2, RemainingTime1, RemainingTime2, backgroundText, readyText, gameOverUI;
    public Color player1Color, player2Color;
    GameObject startText1, startText2, readyText1, readyText2;
    bool hasStarted = false, countdown = false, fullStart = false;
    public string backScene;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = initialTime;
        Time.timeScale = 0;
        //Start UI
        float height = backgroundText.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        startText1 = Instantiate(backgroundText, transform);
        startText1.transform.position = new Vector2(0, -height / 1.6f);
        startText1.GetComponentInChildren<TextMeshProUGUI>().text = "Move your pusher <sprite=\"Icons\" name=bluePuck> and send the puck <sprite=\"Icons\" name=puck> into your opponent goal !";
        startText2 = Instantiate(backgroundText, transform);
        startText2.transform.position = new Vector2(0, height / 1.6f);
        startText2.transform.rotation = new Quaternion(0, 0, 180, 0);
        startText2.GetComponentInChildren<TextMeshProUGUI>().text = "Move your pusher <sprite=\"Icons\" name=redPuck> and send the puck <sprite=\"Icons\" name=puck> into your opponent goal !";
    }

    // Update is called once per frame
    void Update()
    {
        GameIntro();
        if (remainingTime <= 0)
            GameOver();
        if (GameIsEnded && Input.touchCount != 0)
            SceneManager.LoadScene(backScene);
    }

    private void FixedUpdate()
    {
        remainingTime -= Time.deltaTime;
    }
    void GameOver()
    {
        gameOverUI.SetActive(true);
        GameIsEnded = true;
        Time.timeScale = 0;
        if (GetWinner() != null)
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Winner is\n" + GetWinner().GetName() + " !";
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().color = GetWinner().GetColor();
        }
        else
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Equality !";
    }
    void GameIntro()
    {
        if (Input.touchCount != 0 && !hasStarted)
        {
            hasStarted = true;
            readyText1 = Instantiate(readyText, transform);
            readyText1.GetComponent<TextMeshProUGUI>().color = player1Color;
            readyText1.transform.position = startText1.transform.position;
            readyText1.transform.rotation = startText1.transform.rotation;
            readyText2 = Instantiate(readyText, transform);
            readyText2.transform.position = startText2.transform.position;
            readyText2.transform.rotation = startText2.transform.rotation;
            readyText2.GetComponent<TextMeshProUGUI>().color = player2Color;
            Destroy(startText2);
            Destroy(startText1);
            Time.timeScale = 1;
        }
        if (hasStarted == true && remainingTime <= initialTime - 3f && !countdown)
        {
            countdown = true;
            remainingTime = initialTime;
        }
        if (remainingTime <= initialTime - 3f && countdown && !fullStart)
        {
            fullStart = true;
            remainingTime = initialTime;
        }
        if (fullStart)
        {

            RemainingTime1.GetComponent<TextMeshProUGUI>().text =  Mathf.RoundToInt(remainingTime).ToString();
            RemainingTime2.GetComponent<TextMeshProUGUI>().text =  Mathf.RoundToInt(remainingTime).ToString();
        }
    }

    MolePlayer GetWinner()
    {
        if (player1.GetComponent<MolePlayer>().GetScore() > player2.GetComponent<MolePlayer>().GetScore())
            return player1.GetComponent<MolePlayer>();
        else if (player1.GetComponent<MolePlayer>().GetScore() < player2.GetComponent<MolePlayer>().GetScore())
            return player2.GetComponent<MolePlayer>();
        else
            return null;
    }

    public static bool GetGameIsEnded()
    {
        return GameIsEnded;
    }
}
