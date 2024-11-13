using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCapper : MonoBehaviour
{
    public int desiredFPS;
    public int vsync = 0;
    public bool change;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (desiredFPS < 10)
        {
            desiredFPS = 10;
        }
        Application.targetFrameRate = desiredFPS;
        QualitySettings.vSyncCount = vsync;
    }

    // Update is called once per frame
    void Update()
    {
        if(change == true)
        {
            change = false;
            Application.targetFrameRate = desiredFPS;
            QualitySettings.vSyncCount = vsync;
        }
    }
}
