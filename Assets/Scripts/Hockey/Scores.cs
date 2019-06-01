using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Scores : MonoBehaviour
{
    float MiddleDivider;
    private Vector2 vecb, vecr, puckinit,puckgoal;
    public GameObject player1, player2, puck;

    // Start is called before the first frame update
    void Start()
    {
        puckinit = puck.transform.position;
        vecb = player1.transform.position;
        vecr = player2.transform.position;
    }


    void OnCollisionEnter2D(Collision2D goal)
    {

        if (goal.gameObject.name=="puck" && (goal.otherCollider is BoxCollider2D))
        {
            puckgoal = new Vector2(goal.gameObject.transform.position.x, goal.gameObject.transform.position.y);
            //score
            if (puckgoal.x > puckinit.x)
            {
                player1.GetComponent<MolePlayer>().AddScore(1);
            }
            else if (puckgoal.x < puckinit.x)
            {
                player2.GetComponent<MolePlayer>().AddScore(1);
            }
            //reset
            goal.transform.position = new Vector2(0, 0);
            goal.gameObject.GetComponent<MovePuck>().initdirec();
            player1.transform.position = vecb;
            player2.transform.position = vecr;

        }
    }



}
