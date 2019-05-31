using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    Vector2 pointA, pointB, direction;
    float modifier=0;
    // Start is called before the first frame update
    void Start()
    {
        pointA =pointB= gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position != pointB)
        {
            pointA = pointB;
            pointB = transform.position;
            direction = (pointB - pointA).normalized;
        }

    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public float GetModifier()
    {
        return modifier;
    }
    public void SetModifier(float value)
    {
        modifier=value;
    }
}
