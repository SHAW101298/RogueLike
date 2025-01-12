using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class INIT : MonoBehaviour
{
    public bool FOOTrigger;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Init Start");
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
    }

    private void OnPlayerConnected()
    {
        Debug.Log("On Player Connected");
    }

}



