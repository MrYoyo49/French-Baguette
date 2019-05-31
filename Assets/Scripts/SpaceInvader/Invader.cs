using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    float baseSpeed = 0.3f, speed;
    int score=10;
    Vector2 nextPosition, limitScreenSize;
    public Camera myCamera;
    public GameObject explodedPart;
    bool HasBeenBoosted=false;
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
        if (HasBeenBoosted)
        {
            inv1.GetComponent<ExplodedInvader>().Boost();
            inv2.GetComponent<ExplodedInvader>().Boost();
            inv3.GetComponent<ExplodedInvader>().Boost();
        }
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
    public void ReverseSpeed(bool reverse)
    {
        if (reverse)
        {
            baseSpeed *= -1;
        }
    }
    public void Boost()
    {
            baseSpeed *= 3;
        HasBeenBoosted = true;
    }
}
