using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SpaceInvaderGenerateInvaders : MonoBehaviour
{

    float nextSpawn = 1f;
    public GameObject invader, ship1,ship2;
    public Camera myCamera;
    float initialTime = 60, remainingTime;
    static bool GameIsEnded = false;
    public GameObject playerObject;
    GameObject player1, player2;
    public Color player1Color, player2Color;
    public GameObject TextGO1, TextGO2, RemainingTime, secLeftUI, backgroundText, readyText, gameOverUI;
    public Sprite player1Sprite, player2Sprite;
    GameObject startText1, startText2, readyText1, readyText2;
    bool hasStarted = false, countdown = false, fullStart = false, lastSprintEnabled = false;
    public string backScene;
    private void Awake()
    {
        remainingTime = initialTime;
        Time.timeScale = 0;
        player1 = Instantiate(playerObject);
        player1.GetComponent<MolePlayer>().SetValues(1, player1Color, 0, "Player 1", TextGO1, player1Sprite);
        player2 = Instantiate(playerObject);
        player2.GetComponent<MolePlayer>().SetValues(0, player2Color, 0, "Player 2", TextGO2, player2Sprite);
        //Start UI
        float height = backgroundText.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        startText1 = Instantiate(backgroundText, transform);
        startText1.transform.position = new Vector2(0, -height / 1.6f);
        startText1.GetComponentInChildren<TextMeshProUGUI>().text = "Move <sprite=\"Icons\" name=blueFork> by touching the screen and shoot <sprite=\"Icons\" name=bacon>by releasing !";
        startText2 = Instantiate(backgroundText, transform);
        startText2.transform.position = new Vector2(0, height / 1.6f);
        startText2.transform.rotation = new Quaternion(0, 0, 180, 0);
        startText2.GetComponentInChildren<TextMeshProUGUI>().text = "Move <sprite=\"Icons\" name=redFork> by touching the screen and shoot <sprite=\"Icons\" name=bacon>by releasing !";
        ship1.GetComponent<Ship>().myPlayer = player1;
        ship2.GetComponent<Ship>().myPlayer = player2;
    }

    // Update is called once per frame
    void Update()
    {
        GameIntro();
        if (lastSprintEnabled == false && remainingTime <= 30) //enable lastSprint which is faster
        {
            lastSprintEnabled = true;
            //Instantiate(secLeftUI, transform);
            //globalSpeedModifier = 1.5f;
            nextSpawn = 2;
             //player1.HideScore(true);
             //player2.HideScore(true);
        }
        if (remainingTime <= 0)
            GameOver();
        if (GameIsEnded && Input.touchCount != 0)
            SceneManager.LoadScene(backScene);
    }
    private void FixedUpdate()
    {
        if (fullStart)
            nextSpawn -= Time.deltaTime;
        remainingTime -= Time.deltaTime;
        if (nextSpawn <= 0)
        {
            nextSpawn = 1f;
            SpawnInvader(invader);
        }

    }
    private void SpawnInvader(GameObject newInvader)
    {
        Vector2 position;
        float rotation = 0;
        bool reverse = false;
        if (Random.Range(0, 2) < 1)
        {
            position = new Vector2(-8, -1);
        }
        else
        {
            rotation = 90;
            position = new Vector2(8, 1);
            reverse = true;
        }
        GameObject inv=Instantiate(newInvader,position,new Quaternion(0,0,rotation,0));
        inv.GetComponent<Invader>().myCamera = myCamera;
        inv.GetComponent<Invader>().ReverseSpeed(reverse);
        if (Random.Range(0f, 1f) >= 0.9f)
        {
            inv.GetComponent<Invader>().Boost();
        }
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
            RemainingTime.GetComponent<TextMeshProUGUI>().text = "Time : " + Mathf.RoundToInt(remainingTime).ToString();
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        GameIsEnded = true;
        Time.timeScale = 0;
        if (GetWinner() != null)
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Winner is\n" + GetWinner().GetComponent<MolePlayer>().GetName() + " !";
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().color = GetWinner().GetComponent<MolePlayer>().GetColor();
        }
        else
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Equality !";
    }
    public bool HasStarted()
    {
        return fullStart;
    }
    GameObject GetWinner()
    {
         if (player1.GetComponent<MolePlayer>().GetScore() > player2.GetComponent<MolePlayer>().GetScore())
             return player1;
         else if (player1.GetComponent<MolePlayer>().GetScore() < player2.GetComponent<MolePlayer>().GetScore())
             return player2;
         else
             return null;
    }
    public static bool GetGameIsEnded()
    {
        return GameIsEnded;
    }
}
