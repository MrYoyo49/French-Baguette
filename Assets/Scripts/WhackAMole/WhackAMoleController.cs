using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class WhackAMoleController : MonoBehaviour
{
    bool multiplayerModeEnabled = true, lastSprintEnabled = false;
    float initialTime = 90, remainingTime;
    private float nextSpawn = 1;
    private Hole[] holesList;
    MolePlayer player1, player2;
    public Sprite player1moleSprite, player2moleSprite;
    public MolePlayer playerObject;
    public GameObject TextGO1, TextGO2, RemainingTime, secLeftUI, backgroundText, readyText;
    public Canvas canvas;
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
        player1.GetComponent<MolePlayer>().SetValues(1, new Color(255, 0, 0), 0, TextGO1,player1moleSprite);
        player2 = Instantiate(playerObject);
        player2.GetComponent<MolePlayer>().SetValues(0, new Color(0, 60, 255), 0, TextGO2,player2moleSprite);
        //Start UI
        float height = backgroundText.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        startText1 = Instantiate(backgroundText, transform);
        startText1.transform.position = new Vector2(0, - height / 1.6f);
        startText1.GetComponentInChildren<TextMeshProUGUI>().text = "Touch the moles wearing your helmet <sprite name=" + player1Helmet + ">";
        startText2 = Instantiate(backgroundText, transform);
        startText2.transform.position = new Vector2(0, height / 1.6f);
        startText2.transform.rotation = new Quaternion(0, 0, 180, 0);
        startText2.GetComponentInChildren<TextMeshProUGUI>().text = "Touch the moles wearing your helmet <sprite name="+player2Helmet+">";

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
        if (Input.touchCount != 0 && !hasStarted)
        {
            hasStarted = true;
            readyText1 = Instantiate(readyText,transform);
            //readyText1.GetComponent<TextMeshProUGUI>().color = player1Color;
            readyText2 = Instantiate(readyText, transform);
            readyText2.transform.position = startText2.transform.position;
            readyText2.transform.rotation = startText2.transform.rotation;
            readyText2.GetComponent<TextMeshProUGUI>().color = player2Color;
            Destroy(startText2);
            Destroy(startText1);
            Time.timeScale = 1;
        }
        if (hasStarted==true && remainingTime<=initialTime-3f && !countdown)
        {
            countdown = true;
            remainingTime = initialTime;
        }
        if ( remainingTime <= initialTime - 3f && countdown)
        {
            fullStart = true;
            remainingTime = initialTime;
        }
        if (fullStart)
            RemainingTime.GetComponent<TextMeshProUGUI>().text = "Time : " + Mathf.RoundToInt(remainingTime).ToString();
        if (lastSprintEnabled == false && remainingTime <= 30) //enable lastSprint which is faster
        {
            lastSprintEnabled = true;
            Instantiate(secLeftUI,transform);
            globalSpeedModifier = 1.5f;
            nextSpawn = 2;
        }
        if (remainingTime <= 0)
            GameOver();
        if (nextSpawn <0)
            SpawnMoles(holesList);
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
        SceneManager.LoadScene(backScene);
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
                nextSpawn = 0.8f;
            else if (rdn < 0.8f)
                nextSpawn = 0.4f;
            else if (rdn < 0.95f)
                nextSpawn = 0;
            else
                nextSpawn = 1.5f;
        }
        else
        {
            if (rdn < 0.5f)
                nextSpawn = 0.4f;
            else if (rdn < 0.7f)
                nextSpawn = 0.2f;
            else if (rdn < 0.9f)
                nextSpawn = 0;
            else
                nextSpawn = 1;
        }


    }
    public void SetModeMultiplayer(bool enable)
    {
        multiplayerModeEnabled = enable;
    }
}
