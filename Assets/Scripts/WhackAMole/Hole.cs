using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    bool full = false;
    public GameObject mole;
    // Start is called before the first frame update
    void Start()
    {
       // Instantiate(bottom,new Vector2 (transform.position.x,transform.position.y-0.3f), transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFull()
    {
        return full;
    }
    public void SetEmpty()
    {
        full = false;
    }
    public void SpawnAMole(MolePlayer player)
    {
        full = true;
        GameObject spawnedMole = Instantiate(mole,transform);
        spawnedMole.GetComponent<Mole>().SetHole(this);
        spawnedMole.transform.position = new Vector2(this.transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().sprite.bounds.size.y/2 - spawnedMole.GetComponent<SpriteRenderer>().sprite.bounds.size.y/2);
        spawnedMole.GetComponent<Mole>().SetPlayer(player);
        spawnedMole.GetComponent<SpriteRenderer>().sprite = player.GetSprite();
    }

}
