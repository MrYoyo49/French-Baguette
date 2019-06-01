using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Challenge : MonoBehaviour
{
    public static List<string> gameorder;
    public List<string> gameorder2;
    public static List<int> counts;
    public  List<int> counts2;
    List<string> gamesList = new List<string>();
    public Sprite space, whack, hockey;
    public GameObject un, deux, trois;

    public void rando()
    {
        gamesList.Add("Hockey");
        gamesList.Add("SpaceInvader");
        gamesList.Add("WhackAMole");
        Random rnd = new Random();
        for (int i = 0; i < 3; i++)
        {
            int num = rnd.Next(gamesList.Count);
            gameorder2.Add(gamesList[num]);
            if (i == 0)
            {
                if (gamesList[num] == "SpaceInvader")
                {
                    un.GetComponent<Image>().sprite = space;
                    un.SetActive(true);
                    gamesList.Remove("SpaceInvader");
                }
                else if (gamesList[num] == "Hockey")
                {
                    un.GetComponent<Image>().sprite = hockey;
                    un.SetActive(true);
                    gamesList.Remove("Hockey");
                }
                else if (gamesList[num] == "WhackAMole")
                {
                    un.GetComponent<Image>().sprite = whack;
                    un.SetActive(true);
                    gamesList.Remove("WhackAMole");
                }
            }
            else if (i == 1)
            {
                if (gamesList[num] == "SpaceInvader")
                {
                    deux.GetComponent<Image>().sprite = space;
                    deux.SetActive(true);
                    gamesList.Remove("SpaceInvader");
                }
                else if (gamesList[num] == "Hockey")
                {
                    deux.GetComponent<Image>().sprite = hockey;
                    deux.SetActive(true);
                    gamesList.Remove("Hockey");
                }
                else if (gamesList[num] == "WhackAMole")
                {
                    deux.GetComponent<Image>().sprite = whack;
                    deux.SetActive(true);
                    gamesList.Remove("WhackAMole");
                }
            }
            else if (i == 2)
            {
                if (gamesList[num] == "SpaceInvader")
                {
                    trois.GetComponent<Image>().sprite = space;
                    trois.SetActive(true);
                    gamesList.Remove("SpaceInvader");

                }
                else if (gamesList[num] == "Hockey")
                {
                    trois.GetComponent<Image>().sprite = hockey;
                    trois.SetActive(true);
                    gamesList.Remove("Hockey");
                }
                else if (gamesList[num] == "WhackAMole")
                {
                    trois.GetComponent<Image>().sprite = whack;
                    trois.SetActive(true);
                    gamesList.Remove("WhackAMole");
                }
            }
        }
        counts2.Add(0);
        counts2.Add(0);
        counts2.Add(0);
        gameorder = gameorder2;
        counts = counts2;
    }

    public void launch1()
    {
        SceneManager.LoadScene(gamerd());
    }

    public static string gamerd()
    {
        string val;
        if (counts[0]==3)
        {
            val = "Winner Challenge";
            return val;
        }
        val = gameorder[counts[0]];
        return val;                
    }

    public static List<int> setval(string winner)
    {
        if (counts[0] < 3)
        {
            counts[0] += 1;
        }
        if (winner=="blue")
        {
            counts[1] += 1;
        }
        else if (winner == "red")
        {
            counts[2] += 1;
        }
        else if (winner == "equal")
        {
            counts[1] += 1;
            counts[2] += 1;
        }
        return counts;
    }

    public static int geti(int i)
    {
        return counts[i];
    }


}
