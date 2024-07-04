using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QUICKGAMEENTER : MonoBehaviour
{
    public NetworkManager networkManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BTN_QuickStart()
    {
        Debug.Log("Start Hosting");
        networkManager.enabled = true;
        networkManager.StartHost();
        networkManager.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        
    }


}

