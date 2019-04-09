using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvaderGenerateInvaders : MonoBehaviour
{
    float nextSpawn = 1f;
    public GameObject invader;
    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        nextSpawn -= Time.deltaTime;
        if (nextSpawn <= 0)
        {
            nextSpawn = 0.8f;
            SpawnInvader(invader);
        }

    }
    private void SpawnInvader(GameObject newInvader)
    {
        GameObject inv=Instantiate(newInvader, new Vector2(-8,0),new Quaternion());
        inv.GetComponent<Invader>().myCamera = myCamera;
    }
}
