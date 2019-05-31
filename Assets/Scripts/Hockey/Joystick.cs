using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Joystick : MonoBehaviour
{
    public Transform player1, player2;
    private bool touch1Assigned = false, touch2Assigned = false;
    float MiddleDivider, nomanslandtolerance, speed = 10f;
    int touch1, touch2;
    public GameObject joystick1, joystick2;
    GameObject circle1, circle2, outerCircle1, outerCircle2;
    Vector2 touch1init, touch1mod, touch2init, touch2mod;
    float modifier;
    void Start()
    {
        MiddleDivider = Screen.width / 2;
        nomanslandtolerance = 0.2f;
        GetChildren(joystick1,ref circle1,ref outerCircle1);
        GetChildren(joystick2,ref circle2,ref outerCircle2);
    }

    void GetChildren(GameObject joystick, ref GameObject circle, ref GameObject outerCircle)
    {
        foreach (Transform child in joystick.transform) 
        {
            if (child.name == "circle")
            {
                circle = child.gameObject;
            }
            else if (child.name == "outerCircle")
            {
                outerCircle = child.gameObject;
            }
            else
            {
                Debug.Log("Nothing assigned");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.touchCount != 0)
        {
            foreach (Touch touch in Input.touches)
            {
                
                if (touch.position.x <= MiddleDivider * (1 - nomanslandtolerance) && ((touch2Assigned && Input.touches.Length > touch2 )? touch.fingerId != Input.touches[touch2].fingerId:true))
                {
                    if (!touch1Assigned)
                    {
                        touch1 = touch.fingerId;
                        touch1Assigned = true;
                        touch1init = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                        activeJoystick(ref joystick1, ref circle1, ref outerCircle1, touch1init, touch1init);
                    }
                    else
                    {
                        touch1mod = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                        activeJoystick(ref joystick1, ref circle1, ref outerCircle1, touch1mod, touch1init);
                    }

                }
                else if (touch.position.x >= MiddleDivider * (1 + nomanslandtolerance) && ((touch1Assigned && Input.touches.Length > touch1) ? touch.fingerId != Input.touches[touch1].fingerId : true))
                {
                    if (!touch2Assigned){
                        touch2 = touch.fingerId;
                        touch2Assigned = true;
                        touch2init = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                        activeJoystick(ref joystick2, ref circle2, ref outerCircle2, touch2init, touch2init);
                    }
                    else
                    {
                        touch2mod = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                        activeJoystick(ref joystick2, ref circle2, ref outerCircle2, touch2mod, touch2init);
                    }
                }

            }

            if (touch1Assigned)
            {
                Move(touch1init, touch1mod, player1, circle1);
            }
            else
            {
                joystick1.SetActive(false);
            }

            if (touch2Assigned)
            {
                Move(touch2init, touch2mod, player2, circle2);
            }
            else
            {
                joystick2.SetActive(false);
            }

            if (touch1Assigned && Input.touches.Length>touch1)
            {
                if (Input.touches[touch1].phase == TouchPhase.Ended || Input.touches[touch1].phase == TouchPhase.Canceled)
                        touch1Assigned = false;
            }

            if (touch2Assigned && Input.touches.Length > touch2)
            {
                if (Input.touches[touch2].phase == TouchPhase.Ended || Input.touches[touch2].phase == TouchPhase.Canceled)
                touch2Assigned = false;
            }
        }



    }

    void Move(Vector2 touchInit, Vector2 touchFin, Transform player, GameObject circle)
    {
        Vector2 offset = touchFin - touchInit;  
        Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
        modifier = Vector2.Lerp(Vector2.zero, Vector2.one, direction.magnitude).magnitude;
        player.GetComponent<Direction>().SetModifier(modifier);
        player.Translate(direction * speed *modifier * Time.deltaTime);
        circle.transform.position = new Vector2(touchInit.x + direction.x, touchInit.y + direction.y);
    }
    void activeJoystick(ref GameObject joystick,ref GameObject circle,ref GameObject outerCircle, Vector2 pointInner, Vector2 pointOuter)
    {
        circle.transform.position = pointInner; 
        outerCircle.transform.position = pointOuter;
        joystick.SetActive(true);
    }





}
