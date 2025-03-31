using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_SimpleTextPopUp : MonoBehaviour
{
    #region
    public static UI_SimpleTextPopUp Instance;
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

    public TMP_Text textField;
    public GameObject window;

    [Header("Disappear Timer")]
    [SerializeField] float hidingdelay = 0.1f;
    [SerializeField] float hidingTimer;

    public void ShowText(string text)
    {
        textField.text = text;
        ShowWindow();
    }
    public void ShowWindow()
    {
        hidingTimer = hidingdelay;
        window.SetActive(true);
    }
    public void HideWindow()
    {
        window.SetActive(false);
    }

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
}
