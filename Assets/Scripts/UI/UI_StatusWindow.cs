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
    public PlayerData player;
    public GameObject weaponWindow;
    public GameObject blessingsWindow;
    public GameObject statusWindow;
    [Header("Weapons")]
    [SerializeField] UI_WeaponInformation weapon1;
    [SerializeField] UI_WeaponInformation weapon2;
    [Header("Blessings")]
    [SerializeField] GameObject blessingsContent;
    [SerializeField] GameObject blessingPrefab;

    public void ShowWeaponWindow()
    {
        weaponWindow.SetActive(true);
        blessingsWindow.SetActive(false);
        blessingsWindow.SetActive(false);
    }
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
    public void UpdateBlessingsData()
    {
        foreach(Transform child in blessingsContent.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject temp;
        UI_BlessingInfo tempInfo;
        Blessing_Base tempBlessing;
        for(int i = 0; i < player.blessings.list.Count;i++)
        {
            temp = Instantiate(blessingPrefab);
            temp.transform.SetParent(blessingsContent.transform);
            temp.gameObject.transform.localScale = Vector3.one;
            tempInfo = temp.GetComponent<UI_BlessingInfo>();
            tempBlessing = player.blessings.list[i];
            tempInfo.UpdateInfo(tempBlessing.title, tempBlessing.GetDescription(), 0);
        }
    }
    public void UpdateStatsData()
    {

    }

}
