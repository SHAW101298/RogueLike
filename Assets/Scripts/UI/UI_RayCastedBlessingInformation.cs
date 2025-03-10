using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_RayCastedBlessingInformation : MonoBehaviour
{
    #region
    public static UI_RayCastedBlessingInformation Instance;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public GameObject blessingInfoWindow;
    public TMP_Text blessingName;
    public TMP_Text blessingDescription;

    [Header("Disappear Timer")]
    [SerializeField] float hidingdelay = 0.1f;
    [SerializeField] float hidingTimer;

    void Update()
    {
        if (hidingTimer > 0)
        {
            hidingTimer -= Time.deltaTime;
            if (hidingTimer <= 0)
            {
                HideWindow();
            }
        }
    }
    public void ShowData(Blessing_Base blessing)
    {
        ShowWindow();
        blessingName.text = blessing.title;
        blessingDescription.text = blessing.GetDescription();
    }
    public void HideWindow()
    {
        blessingInfoWindow.SetActive(false);
    }
    public void ShowWindow()
    {
        hidingTimer = hidingdelay;
        blessingInfoWindow.SetActive(true);
    }
}
