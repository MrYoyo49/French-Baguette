using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Scores : MonoBehaviour
{
    float MiddleDivider;
    private Vector2 vecb, vecr;
    public GameObject player1, player2;

    // Start is called before the first frame update
    void Start()
    {
        MiddleDivider = Screen.width / 2;
        vecb = player1.transform.position;
        vecr = player2.transform.position;
    }


    void OnCollisionEnter2D(Collision2D goal)
    {

        if (goal.gameObject.name=="puck" && (goal.GetContact(0).otherCollider is BoxCollider2D))
        {
            //score
            if (goal.gameObject.transform.position.x < MiddleDivider)
            {
                player1.GetComponent<MolePlayer>().AddScore(1);
            }
            else if (goal.gameObject.transform.position.x > MiddleDivider)
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
