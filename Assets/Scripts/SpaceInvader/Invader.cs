using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    float baseSpeed = 0.2f, speed;
    int score=10;
    Vector2 nextPosition, limitScreenSize;
    public Camera myCamera;
    public GameObject explodedPart;
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
        nextPosition.x += speed*Time.timeScale;
        transform.position = nextPosition;
       // if (transform.position.x - GetComponent<SpriteRenderer>().sprite.bounds.size.x >= limitScreenSize.x || transform.position.x < -limitScreenSize.x - GetComponent<SpriteRenderer>().sprite.bounds.size.x)
        //    Object.Destroy(gameObject);
            
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void SetBaseSpeed(float speed)
    {
        baseSpeed = speed;
    }

    public int explode(float sign)
    {
        if (sign > 0)
        {
            sign = 0;
        }
        else
        {
            sign = 1;
        }
        GameObject inv1 = Instantiate(explodedPart, transform.position, Quaternion.AngleAxis(180 * sign, Vector3.forward));
        GameObject inv2 = Instantiate(explodedPart, transform.position, Quaternion.AngleAxis(45+180*sign, Vector3.forward));
        GameObject inv3 = Instantiate(explodedPart, transform.position, Quaternion.AngleAxis(-45+180*sign,Vector3.forward));
        Destroy(gameObject);
        return score;
    }

    public Vector2 Rotate(Vector2 vec, float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector2(vec.x*Mathf.Cos(angle),vec.y* Mathf.Sin(angle));
    }

    public int GetScore()
    {
        return score;
    }

}
