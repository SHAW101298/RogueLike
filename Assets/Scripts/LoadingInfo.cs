using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingInfo : MonoBehaviour
{
    #region
    public static LoadingInfo Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public GameObject loadingWindow;
    public Text txt;
    public bool debug;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += ChangedScene;
    }

    private void ChangedScene(Scene newScene, LoadSceneMode mode)
    {
        if(newScene.name.Equals("SampleScene"))
        {
            DisableInfo();
        }
    }



    public void UpdateCurrentProgress(string info)
    {
        if(debug == true)
        {
            Debug.Log(info);
        }
        txt.text = info;
    }

    public void EnableInfo()
    {
        loadingWindow.SetActive(true);
    }
    public void DisableInfo()
    {

        loadingWindow.SetActive(false);
    }
}
