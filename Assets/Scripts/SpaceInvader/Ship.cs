using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public GameObject myPlayer, ammunition;
    float speed = 0.3f,shoot_coolddown, shoot_coolddown_time=1f;
    Vector2 limitScreenSize;
    // Start is called before the first frame update
    void Start()
    {
        shoot_coolddown = shoot_coolddown_time;
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown(ref shoot_coolddown);
    }
    public float GetSpeed()
    {
        return speed;
    }


    public void Shoot()
    {
        if (shoot_coolddown == 0)
        {
            GameObject sh = Instantiate(ammunition);
            sh.transform.position = transform.position;
            sh.transform.rotation = transform.rotation;
            if (transform.rotation == new Quaternion (0,0, 180,0))
                 sh.GetComponent<Ship_Ammunition>().ReverseSpeed();
            sh.GetComponent<Ship_Ammunition>().SetLimitSreenSize(limitScreenSize);
            shoot_coolddown = shoot_coolddown_time;
        }

    }
    public void cooldown(ref float value)
    {
        if (value > 0)
            value -= Time.deltaTime;
        else
            value = 0;
    }

    public void SetLimitSreenSize(Vector2 v)
    {
        limitScreenSize = v;
    }
}
