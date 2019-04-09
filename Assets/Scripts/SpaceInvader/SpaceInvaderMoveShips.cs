using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvaderMoveShips : MonoBehaviour
{
    public GameObject ship1, ship2;
    public Camera myCamera;
    float MiddleDivider;
    Touch touch1, touch2;//touch of player one and two
    bool touch1Assigned = false , touch2Assigned = false;
    int previousTouchCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        MiddleDivider = Screen.height /2;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("one " + touch1Assigned + " two " + touch2Assigned);
        if (Input.touchCount!=0)
        {
            //assign touch on each side of screen if not already done
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.y <= MiddleDivider)
                {
                    Debug.Log("FingerPlayer1");
                    if (touch1Assigned == false)
                    {
                        touch1 = touch;
                        touch1Assigned = true;
                    }
                }
                else
                {
                    if (touch2Assigned == false)
                    {
                        touch2 = touch;
                        touch2Assigned = true;
                    }
                }
            }
            // move the ships according to location if screen is touched
            if (touch1Assigned)
            {
                MoveShip(ship1, touch1.position);
            }
            if (touch2Assigned)
            {
                MoveShip(ship2, touch2.position);
            }
        }

        if (touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Moved || previousTouchCount != Input.touchCount)
            touch1Assigned = false;
        if (touch2.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Moved || previousTouchCount != Input.touchCount)
            touch2Assigned = false;

        previousTouchCount = Input.touchCount;

    }

    private void MoveShip(GameObject ship, Vector2 coordinate)
    {
        Vector2 TouchPosition = myCamera.ScreenToWorldPoint(coordinate);//convert in world unit
        if (TouchPosition.x < ship.transform.position.x)
        { ship.transform.position = new Vector2(ship.transform.position.x - ship.GetComponent<Ship>().GetSpeed(), ship.transform.position.y); Debug.Log("Left"); }
        else if (TouchPosition.x > ship.transform.position.x)
        { ship.transform.position = new Vector2(ship.transform.position.x + ship.GetComponent<Ship>().GetSpeed(), ship.transform.position.y); Debug.Log("Right"); }
        }
}
