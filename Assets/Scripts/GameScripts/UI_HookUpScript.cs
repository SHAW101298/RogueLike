using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HookUpScript : MonoBehaviour
{
    #region
    public static UI_HookUpScript Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Debug.Log("Instance Created");
            Instance = this;
        }
    }
    #endregion
    [Header("UI Objects")]
    public Text magazineCurrent;
    public Text ammoCurrent;
    public Image staminaBar;
    public GameObject characterSelector;
    public GameObject workshop;
    // Start is called before the first frame update
}
