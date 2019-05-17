using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Ammunition : MonoBehaviour
{
    float std_Speed = 1.5f;
    Vector2 limitScreenSize;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,std_Speed,0)*Time.deltaTime ;
        Destroy_OutofScreen();
    }

    void Destroy_OutofScreen()
    {
        if (transform.position.y - GetComponent<SpriteRenderer>().sprite.bounds.size.y >= limitScreenSize.y || transform.position.y < -limitScreenSize.y - GetComponent<SpriteRenderer>().sprite.bounds.size.y)
            Object.Destroy(gameObject);
    }

    public void SetLimitSreenSize(Vector2 v)
    {
        limitScreenSize = v;
    }

    public void ReverseSpeed()
    {
        std_Speed *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch");
        Destroy(gameObject);
    }
}
