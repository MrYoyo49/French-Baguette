using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class WhackAMoleController : MonoBehaviour
{
    bool multiplayerModeEnabled = true, lastSprintEnabled = false;
    float initialTime = 60, remainingTime;
    private float nextSpawn = 1;
    private Hole[] holesList;
    static bool GameIsEnded = false, challbool=false;
    MolePlayer player1, player2;
    public Sprite player1moleSprite, player2moleSprite;
    public MolePlayer playerObject;
    public GameObject TextGO1, TextGO2, RemainingTime, secLeftUI, backgroundText, readyText, gameOverUI, gameOverUI2;
    public Color player1Color, player2Color;
    GameObject startText1, startText2, readyText1,readyText2;
    public string player1Helmet, player2Helmet;
    float playerNumMod = 0.5f;
    bool hasStarted = false, countdown=false, fullStart =false;
    public static float globalSpeedModifier = 1f;
    public string backMultiScene, backSoloScene;
    string backScene;

    // Start is called before the first frame update
    private void Awake()
    {
        remainingTime = initialTime;
        Time.timeScale=0;
        player1 = Instantiate(playerObject);
        player1.GetComponent<MolePlayer>().SetValues(1, player1Color, 0,"Player 1", TextGO1,player1moleSprite);
        player2 = Instantiate(playerObject);
        player2.GetComponent<MolePlayer>().SetValues(0, player2Color, 0,"Player 2", TextGO2,player2moleSprite);
        //Start UI
        float height = backgroundText.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        startText1 = Instantiate(backgroundText, transform);
        startText1.transform.position = new Vector2(0,  -height / 1.6f);
        startText1.GetComponentInChildren<TextMeshProUGUI>().text = "Touch the moles wearing your helmet <sprite=\"Icons\" name=" + player2Helmet + ">";
        startText2 = Instantiate(backgroundText, transform);
        startText2.transform.position = new Vector2(0,  height / 1.6f);
        startText2.transform.rotation = new Quaternion(0, 0, 180, 0);
        startText2.GetComponentInChildren<TextMeshProUGUI>().text = "Touch the moles wearing your helmet <sprite=\"Icons\" name=" + player1Helmet+">";

    }
    void Start()
    {
        //holesList = GameObject.FindGameObjectsWithTag("Hole");
        holesList = Hole.FindObjectsOfType<Hole>();
      /*  for (int i = 0; i < holesList.Length; i++)
        {
            holeScript[i] = holesList[i].GetComponent<Hole>();
        }*/
        if (multiplayerModeEnabled)
        {
            MultiplayerMode();
        }
        else
        {
            backScene = backSoloScene;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        GameIntro();
        if (lastSprintEnabled == false && remainingTime <= 30) //enable lastSprint which is faster
        {
            lastSprintEnabled = true;
            //Instantiate(secLeftUI,transform);
            globalSpeedModifier = 1.5f;
            nextSpawn = 2;
           // player1.HideScore(true);
            //player2.HideScore(true);
        }
        if (remainingTime <= 0)
            GameOver();
        if (nextSpawn <0)
            SpawnMoles(holesList);
        if (GameIsEnded && Input.touchCount != 0 && challbool == false)
        {
            SceneManager.LoadScene(backScene);
            setboolsfalse();
        }
        else if (GameIsEnded && Input.touchCount != 0 && challbool)
        {
            setboolsfalse();
            Challenge.setval(getwin());
            SceneManager.LoadScene(Challenge.gamerd());
        }
    }

    private void FixedUpdate()
    {
        remainingTime -= Time.deltaTime;
        if (fullStart)
            nextSpawn -= Time.deltaTime;
    }

    void MultiplayerMode()
    {
        backScene = backMultiScene;
    }
    
    void GameOver()
    {
        gameOverUI.SetActive(true);
        gameOverUI2.SetActive(true);
        GameIsEnded = true;
        Time.timeScale = 0;
        if (GetWinner() != null)
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Winner is\n" + GetWinner().GetName() + " !";
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().color = GetWinner().GetColor();
            gameOverUI2.GetComponentInChildren<TextMeshProUGUI>().text = "Winner is\n" + GetWinner().GetName() + " !";
            gameOverUI2.GetComponentInChildren<TextMeshProUGUI>().color = GetWinner().GetColor();
        }
        else
        {
            gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Equality !";
            gameOverUI2.GetComponentInChildren<TextMeshProUGUI>().text = "Equality !";
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
    void SpawnMoles (Hole[] holes)
    {
        MolePlayer molePlayer;
        int holeIndex=0;
        float rdn = Random.Range(0f, 1f);
        if (rdn <= playerNumMod)
        {
            molePlayer = player1;
            playerNumMod -= 0.2f;
        }
        else
        {
            molePlayer = player2;
            playerNumMod += 0.2f;
        }
        int secNumber = 0; 
        do
        {
            secNumber++;
            if (secNumber==4)
            //if #secNumbers holes are already full stop the random and find the first free, 
            //increasing it increase random but might decrease performance
            {
                for (int j = 0; j < holes.Length; j++)
                {
                    if (!holes[j].IsFull())
                    {
                        holeIndex = j;
                        break;
                    }
                }
            }
            else
            {
                holeIndex = Random.Range(0, holes.Length);
            }
        } while (holes[holeIndex].IsFull() && secNumber!=holes.Length-1);
        if (!holes[holeIndex].IsFull())
        {
            holes[holeIndex].SpawnAMole(molePlayer);
        }
        if (!lastSprintEnabled)// set when next mole will be added
        {
            if (rdn < 0.5f) 
                nextSpawn = 0.5f;
            else if (rdn < 0.8f)
                nextSpawn = 0.2f;
            else if (rdn < 0.95f)
                nextSpawn = 0;
            else
                nextSpawn = 1f;
        }
        else
        {
            if (rdn < 0.5f)
                nextSpawn = 0.3f;
            else if (rdn < 0.7f)
                nextSpawn = 0.1f;
            else if (rdn < 0.9f)
                nextSpawn = 0;
            else
                nextSpawn = 0.7f;
        }


    }

    MolePlayer GetWinner()
    {
        if (player1.GetScore() > player2.GetScore())
            return player1;
        else if (player1.GetScore() < player2.GetScore())
            return player2;
        else 
            return null;
    }

    public void SetModeMultiplayer(bool enable)
    {
        multiplayerModeEnabled = enable;
    }
    public static  bool GetGameIsEnded()
    {
        return GameIsEnded;
    }

    public void setboolsfalse()
    {
        hasStarted = countdown = fullStart = lastSprintEnabled = GameIsEnded = false;
    }

    public void chall(bool enable)
    {
        challbool = enable;
    }

    public string getwin()
    {
        if (player1.GetComponent<MolePlayer>().GetScore() > player2.GetComponent<MolePlayer>().GetScore())
            return "blue";
        else if (player1.GetComponent<MolePlayer>().GetScore() < player2.GetComponent<MolePlayer>().GetScore())
            return "red";
        else
            return "equal";
    }
}
