﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INIT : MonoBehaviour
{
    public GunUpgradeDamage damageUpgrade;

    public bool FOOTrigger;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Init Start");
        FOO();
    }

    // Update is called once per frame
    void Update()
    {
        if(FOOTrigger == true)
        {
            FOOTrigger = false;
            FOO();
        }
    }

    void FOO()
    {
        Debug.LogWarning("FOO");
        damageUpgrade = new GunUpgradeDamage();
        damageUpgrade.Roll();
        Debug.Log(damageUpgrade.ToString());
    }
}