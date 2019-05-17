using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 100.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    //public Transform circle;
    //public Transform outerCircle;

    public GameObject circle1;
    public GameObject outerCircle1;

    public GameObject T;
    private bool isTrig = false;


    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && (Input.mousePosition.x <= Screen.width / 2))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            
            circle1.transform.position = pointA;
            outerCircle1.transform.position = pointA ;
            circle1.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle1.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0) && (Input.mousePosition.x <= Screen.width / 2))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);

            circle1.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
        else
        {
            circle1.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle1.GetComponent<SpriteRenderer>().enabled = false;
        }

    }



    void moveCharacter(Vector2 direction)
    {
            player.Translate(direction * speed * Time.deltaTime);

    }
}
