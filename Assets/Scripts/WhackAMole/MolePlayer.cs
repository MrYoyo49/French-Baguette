using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MolePlayer : MonoBehaviour
{
    public int playerNumber = 0;
    public Color playerColor = new Color(255, 0, 0);
    int score = 0;
    public GameObject TextScore;
    Sprite moleSprite;
    TextMeshProUGUI textScoreComponent;
    // Start is called before the first frame update
    void Start()
    {
        textScoreComponent = TextScore.GetComponent<TextMeshProUGUI>();
        
    }
    public void SetValues (int num, Color col, int score, GameObject textUI, Sprite moleSprite)
    {
        playerNumber = num;
        playerColor = col;
        this.score = score;
        TextScore = textUI;
        this.moleSprite = moleSprite;
    }

    public Sprite GetSprite()
    {
        return moleSprite;
    }
    
    // Update is called once per frame
    void Update()
    {
        textScoreComponent.text = score.ToString();
    }

    public void AddScore (int newScore)
    {
        score += newScore;
    }
}
