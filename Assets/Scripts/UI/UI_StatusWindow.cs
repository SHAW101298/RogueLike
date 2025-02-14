using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatusWindow : MonoBehaviour
{
    #region
    public static UI_StatusWindow Instance;
    public void Awake()
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

    public GameObject weaponWindow;
    public GameObject blessingsWindow;
    public GameObject statusWindow;
    [Space(10)]

    [SerializeField] UI_WeaponInformation weapon1;
    [SerializeField] UI_WeaponInformation weapon2;

    public void UpdateWeaponData()
    {
        weapon1.ShowInfo(1);
        weapon2.ShowInfo(2);
    }
    public void ClearData()
    {
        weapon1.ClearWindow();
        weapon2.ClearWindow();
    }

}
