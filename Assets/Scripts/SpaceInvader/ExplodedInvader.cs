using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedInvader : MonoBehaviour
{
    int score=3;
    float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    public int GetScore()
    {
        return score;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
