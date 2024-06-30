using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript1 : MonoBehaviour
{
    #region
    public static TestScript1 ins;
    private void Awake()
    {
         if(ins != null && ins != this)
        {
            Destroy(gameObject);
        }
         else
        {
            ins = this;
        }
    }
    #endregion

    public TestScript2 test2;

    public float timer = 0;
    public float maxTime = 5;
    public bool cycleCompleted;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }



    // Update is called once per frame
    void Update()
    {
        if (cycleCompleted == true)
            return;

        timer += Time.deltaTime;
        if(timer >= maxTime)
        {
            timer = 0;
            cycleCompleted = true;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
