using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvaderMoveShips : MonoBehaviour
{
    public GameObject ship1, ship2;
    public Camera myCamera;
    float MiddleDivider, tolerance,nomanslandtolerance;
    Touch touch1, touch2;//touch of player one and two
    bool touch1Assigned = false , touch2Assigned = false, shoot1loaded = true, shoot2loaded = true; 
    int previousTouchCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        MiddleDivider = Screen.height /2;
        tolerance = 0.3f;
        nomanslandtolerance = 0.2f;
        ship1.GetComponent<Ship>().SetLimitSreenSize(myCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
        ship2.GetComponent<Ship>().SetLimitSreenSize(myCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale==1 && gameObject.GetComponent<SpaceInvaderGenerateInvaders>().HasStarted())
            GetTouch();
    }

    private void MoveShip(GameObject ship, Vector2 coordinate)
    {
        Vector2 TouchPosition = myCamera.ScreenToWorldPoint(coordinate);//convert in world unit
        if (TouchPosition.x +tolerance< ship.transform.position.x)
            { ship.transform.position = new Vector2(ship.transform.position.x - ship.GetComponent<Ship>().GetSpeed() * Time.deltaTime, ship.transform.position.y); }
        else if (TouchPosition.x -tolerance> ship.transform.position.x)
            { ship.transform.position = new Vector2(ship.transform.position.x + ship.GetComponent<Ship>().GetSpeed() * Time.deltaTime, ship.transform.position.y); }
    }

    private void GetTouch()
    {

        if (Input.touchCount != 0)
        {
            //assign touch on each side of screen if not already done
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.y <= MiddleDivider * (1 - nomanslandtolerance))
                {
                    touch1 = touch;
                    touch1Assigned = true;
                    shoot1loaded = true;
                }
                else if (touch.position.y >= MiddleDivider * (1 + nomanslandtolerance))
                {
                    touch2 = touch;
                    touch2Assigned = true;
                    shoot2loaded = true;
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

        if (!touch1Assigned && touch1.phase == TouchPhase.Ended && shoot1loaded)
            ship1.GetComponent<Ship>().Shoot(); shoot1loaded = false;
        if (!touch2Assigned && touch2.phase == TouchPhase.Ended && shoot2loaded)
            ship2.GetComponent<Ship>().Shoot(); shoot2loaded = false;
    }
}
