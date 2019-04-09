using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    TextMeshProUGUI text;
    Animator animator;
    float timer = 1f;
    int count = 3;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Ready ?";
        animator = GetComponent<Animator>();
        //animator.speed = 1/Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0 && count>0)
        {
            GetComponent<Animator>().SetBool("Play", true);
            text.text = count.ToString();
            count--;
            timer = 1f;
        }
        if (count == 0 && timer <=0)
        {
            GetComponent<Animator>().SetBool("Grow", true);
            text.text = "Go !";
            //GetComponent<Animator>().;
            //Destroy(GetComponent<Animator>());
            timer = 1f;
            count--;
        }
        else if (count == -1 && timer <=0)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
    }
}
