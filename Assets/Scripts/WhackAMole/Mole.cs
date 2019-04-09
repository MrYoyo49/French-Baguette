using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mole : MonoBehaviour
{
    Hole myHole;
    MolePlayer myPlayer;
    int baseScore = 100;
    int speedBonusScore = 100;//decrease each fixed amount of time
    public float maxUpLimit, newY, initialY,speedBonusScoreDecreaseRate;
    Sprite mySprite;
    float movingSpeed = 0.04f * WhackAMoleController.globalSpeedModifier; //world unit per second
    bool reachedTop = false;
    AudioSource hitSound;
    //Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>().sprite;
        initialY = newY = this.transform.position.y ;        
        maxUpLimit = Random.Range(initialY+ mySprite.bounds.size.y/2, mySprite.bounds.size.y + initialY); //mole will go between all out and 1/2 of all out
        speedBonusScoreDecreaseRate = movingSpeed * speedBonusScore / (mySprite.bounds.size.y * 2);//
        float speedModifier = Random.Range(0f, 1f);        
        if (speedModifier > 0.9) //10% chance
        {
            movingSpeed *= 2f;
        }
        else if (speedModifier > 0.7f) // 20% chance
        {
            movingSpeed *= 1.5f;
        }
        else if (speedModifier > 0.5f) // 20% chance
        {
            movingSpeed *= 1.2f;
        }
        else if (speedModifier > 0.45f) // 5% chance
        {
            movingSpeed *= 0.8f;
        } // else standard speed // 45% 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        speedBonusScore--;
        if (newY >= maxUpLimit)//check if should stop going up
            reachedTop = true;
        if (!reachedTop)//move up if still inside the hole
            newY += movingSpeed;
        else
        {
            if (newY <= initialY) //destroy if reaches bottom
            {
                myHole.SetEmpty();
                Destroy(gameObject);
            }
            newY -= movingSpeed; // else move down
        }

        this.transform.position = new Vector2(this.transform.position.x, newY);//move the mole
    }

    public void SetHole(Hole hole)
    {
        myHole = hole;
    }
    public void SetPlayer(MolePlayer player)
    {
        myPlayer = player;
    }

    void OnMouseDown()
    {
        myHole.SetEmpty();
        if (myPlayer != null)
        {
            myPlayer.AddScore(baseScore+speedBonusScore);
        }
        //Instantiate(scoreText);
        //hitSound.Play();
        Destroy(gameObject);
    }
}
