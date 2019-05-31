using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuck : MonoBehaviour
{
    Vector2 dirinit, direc;
    float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        dirinit = new Vector2(0,0);
    }



    void OnCollisionEnter2D(Collision2D colli)
    {
        float angle;
        if (colli.gameObject.tag == "Controller")
        {

            direc = colli.gameObject.GetComponent<Direction>().GetDirection();
            angle = Vector2.Angle(direc, colli.GetContact(0).normal);
            float modifier = colli.gameObject.GetComponent<Direction>().GetModifier();
            gameObject.GetComponent<Rigidbody2D>().AddForce(direc * speed*(0.5f+modifier), ForceMode2D.Impulse);
        }
        else
        {
            angle = Vector2.Angle(direc, colli.GetContact(0).normal);
            direc = (Quaternion.AngleAxis(180 - 2 * angle, Vector3.forward) * direc).normalized;
            //gameObject.GetComponent<Rigidbody2D>().AddForce(direc * speed, ForceMode2D.Impulse);
        }

    }

    public void initdirec()
    {
        direc = dirinit;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
