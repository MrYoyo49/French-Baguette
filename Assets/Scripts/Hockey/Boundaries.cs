using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera MainCamera;
    private float collideWidth;
    private float collideHeight;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private float MiddleDivider;
    public string position;

    // Use this for initialization
    void Start()
    {
        MiddleDivider = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, collideHeight, MainCamera.transform.position.z)).x; 
        collideWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        collideHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(collideWidth, collideHeight, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 viewPos = transform.position;
        if (position == "middle")
        {
           // viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        } else if (position == "left")
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, MiddleDivider - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
            transform.position = viewPos;
        } else if (position == "right")
        {
            viewPos.x = Mathf.Clamp(viewPos.x, MiddleDivider+ objectWidth, screenBounds.x * -1 - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
            transform.position = viewPos;
        }

    }
}
