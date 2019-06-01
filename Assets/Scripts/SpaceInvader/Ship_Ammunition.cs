using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Ammunition : MonoBehaviour
{
    float std_Speed = 12f;
    Vector2 limitScreenSize;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,std_Speed,0)*Time.deltaTime ;
        Destroy_OutofScreen();
    }

    void Destroy_OutofScreen()
    {
        if (transform.position.y - GetComponent<SpriteRenderer>().sprite.bounds.size.y >= limitScreenSize.y || transform.position.y < -limitScreenSize.y - GetComponent<SpriteRenderer>().sprite.bounds.size.y)
            Object.Destroy(gameObject);
    }

    public void SetLimitSreenSize(Vector2 v)
    {
        limitScreenSize = v;
    }

    public void ReverseSpeed()
    {
        std_Speed *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Invader>() != null)// if collide with invader
        {
            player.GetComponent<MolePlayer>().AddScore(collision.gameObject.GetComponent<Invader>().GetScore());
            collision.gameObject.GetComponent<Invader>().explode(Mathf.Sign(std_Speed));
        }
        if (collision.gameObject.GetComponent<ExplodedInvader>() != null)// if collide with expolded invader
        {
            player.GetComponent<MolePlayer>().AddScore(collision.gameObject.GetComponent<ExplodedInvader>().GetScore());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<Ship>() != null && collision.gameObject.GetComponent<Ship>().myPlayer!=player)// if collide with fork
        {
            player.GetComponent<MolePlayer>().AddScore(50);
        }
        if (collision.gameObject.GetComponent<Ship>() == null || collision.gameObject.GetComponent<Ship>().myPlayer != player)
            Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
