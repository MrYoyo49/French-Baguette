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

    public Transform BorderZone;

    // Use this for initialization
    void Start()
    {
        collideWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        collideHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(collideWidth, collideHeight, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        transform.position = viewPos;
    }
}
