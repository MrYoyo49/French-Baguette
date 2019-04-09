﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Play Global
    private static KeepMusic instance = null;
    public static KeepMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        
    }
}