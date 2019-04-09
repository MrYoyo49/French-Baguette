using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    float baseSpeed=0.3f, speed,score;
    Vector2 nextPosition, limitScreenSize;
    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        nextPosition = new Vector2(transform.position.x, transform.position.y);
        limitScreenSize = myCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        nextPosition.x += speed;
        transform.position = nextPosition;
        if (transform.position.x - GetComponent<SpriteRenderer>().sprite.bounds.size.x >= limitScreenSize.x || transform.position.x < -limitScreenSize.x - GetComponent<SpriteRenderer>().sprite.bounds.size.x)
            Object.Destroy(gameObject);
            
    }

    public void SetBaseSpeed(float speed)
    {
        baseSpeed = speed;
    }
   
}
