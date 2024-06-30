using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    #region
    public static TestScript2 ins;
    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
        }
    }
    #endregion
    public TestScript1 test1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
