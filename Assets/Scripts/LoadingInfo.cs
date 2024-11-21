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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);


        
    }


    public void UpdateCurrentProgress(string info)
    {
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
