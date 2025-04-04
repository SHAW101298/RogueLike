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
            //Debug.Log("Instance Created");
            Instance = this;
        }
    }
    #endregion
    public PlayerData player;
    [Header("UI Objects")]
    public Text magazineCurrent;
    public Text ammoCurrent;
    public Image staminaBar;
    public Image healthBar;
    public Image shieldBar;
    public Image reloadBar;
    public GameObject reloadWindow;
    public GameObject characterSelector;
    public GameObject workshop;
    public GameObject statusWindow;
    [Header("Status Windows")]
    public GameObject weaponsWindow;
    public GameObject blessingsWindow;
    public GameObject statsWindow;
    // Start is called before the first frame update

    

    public void BTN_CloseWorkShopWindow()
    {
        player.ui.HideWorkShopWindow();
    }
    public void BTN_WeaponsWindow()
    {
        weaponsWindow.SetActive(true);
        blessingsWindow.SetActive(false);
        statsWindow.SetActive(false);
    }
    public void BTN_BlessingsWindow()
    {
        player.ui.UpdateBlessingsContent();
        weaponsWindow.SetActive(false);
        blessingsWindow.SetActive(true);
        statsWindow.SetActive(false);
    }
    public void BTN_StatsWindow()
    {
        weaponsWindow.SetActive(false);
        blessingsWindow.SetActive(false);
        statsWindow.SetActive(true);
    }
}
