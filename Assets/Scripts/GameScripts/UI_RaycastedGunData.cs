using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.PackageManager.UI;
using UnityEngine.UI;

public class UI_RaycastedGunData : MonoBehaviour
{
    #region
    public static UI_RaycastedGunData Instance;
    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Debug.Log("Setting up Instance of raycasted Gun data");
            Instance = this;
        }
    }
    #endregion
    [SerializeField] TMP_Text nameField;
    [SerializeField] TMP_Text damageTypeField;
    [SerializeField] Image damageTypeIcon;
    [SerializeField] TMP_Text damageAmountField;
    [SerializeField] TMP_Text critChanceField;
    [SerializeField] TMP_Text critMultiplierField;
    [SerializeField] TMP_Text afflictionChanceField;
    [SerializeField] TMP_Text speedField;
    [SerializeField] TMP_Text ammoField;
    [Space(10)]
    [SerializeField] GameObject window;
    [SerializeField] RectTransform windowTransform;
    [SerializeField] RectTransform damageParent;
    [SerializeField] VerticalLayoutGroup damageLayout;
    [SerializeField] RectTransform simpleDataParent;
    [SerializeField] RectTransform bonusesParent;
    [SerializeField] VerticalLayoutGroup bonusesLayout;
    [Header("Prefabs")]
    [SerializeField] GameObject damagePrefab;
    [SerializeField] GameObject bonusPrefab;
    [Header("Data")]
    [SerializeField] Gun lastGun;
    [SerializeField] bool activated;
    [Header("Disappear Timer")]
    [SerializeField] float hidingdelay = 0.2f;
    [SerializeField] float hidingTimer;
    [Header("Debug")]
    public bool runFunction;

    // Update is called once per frame
    void Update()
    {
        if(hidingTimer > 0)
        {
            hidingTimer -= Time.deltaTime;
            if(hidingTimer <= 0)
            {
                lastGun = null;
                HideWindow();
            }
        }
    }

    public void ShowGunData(Gun gun)
    {
        hidingTimer = hidingdelay;
        if(lastGun == gun)
        {
            //Debug.Log("Returning");
            return;
        }
        Debug.Log("Gun = " + gun);
        lastGun = gun;
        HideWindow();
        nameField.text = gun.gunName;
        //ShowDamage(gun);
        ShowDamageNew(gun);
        ShowSimpleData(gun);
        ShowBonuses(gun);
        ResizeWindow();
        ShowWindow();
    }
    /* OLD SHOW DAMAGE
    void ShowDamage(Gun gun)
    {
        GameObject tempGO;
        float size = 0;

        foreach (Transform child in damageParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (DamageData dmgData in gun.modifiedStats.damageArray)
        {
            tempGO = Instantiate(damagePrefab);
            tempGO.transform.SetParent(damageParent);
            tempGO.transform.localScale = Vector3.one;
            Transform child = tempGO.transform.Find("Label");
            child.GetComponent<TMP_Text>().text = dmgData.damageType.ToString();
            child = tempGO.transform.Find("Data");
            if(gun.modifiedStats.numberOfProjectiles > 1)
            {
                child.GetComponent<TMP_Text>().text = gun.modifiedStats.numberOfProjectiles + "x" +  dmgData.damage.ToString();
            }
            else
            {
                child.GetComponent<TMP_Text>().text = dmgData.damage.ToString();
            }
            
            size += 25;
        }
        size += damageLayout.padding.top + damageLayout.padding.bottom;
        damageParent.sizeDelta = new Vector2(damageParent.rect.width, size);
    }
    */
    void ShowDamageNew(Gun gun)
    {
        damageTypeField.text = gun.modifiedStats.basedamage.damageType.ToString();
        damageAmountField.text = gun.modifiedStats.basedamage.damage.ToString();
        damageTypeIcon.sprite = IconsManager.Instance.GetAfflictionIcon(gun.modifiedStats.basedamage.damageType);
    }
    void ShowSimpleData(Gun gun)
    {
        critChanceField.text = gun.modifiedStats.critChance.ToString();
        critMultiplierField.text = gun.modifiedStats.critMultiplier.ToString();
        afflictionChanceField.text = gun.modifiedStats.afflictionChance.ToString();
        ammoField.text = gun.modifiedStats.magazineMax.ToString();
    }
    void ShowBonuses(Gun gun)
    {
        GameObject tempGO;
        UI_DataField tempDataFields;
        float newHeight = 0;
        float size = 0;
        TMP_Text textField;
        RectTransform tempTransform;
        foreach (Transform child in bonusesParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (GunUpgradeBase upgrade in gun.gunUpgrades)
        {
            tempGO = Instantiate(bonusPrefab);
            tempGO.transform.SetParent(bonusesParent);
            tempGO.transform.localScale = Vector3.one;
            tempDataFields = tempGO.GetComponent<UI_DataField>();
            tempDataFields.UpdateLabel(upgrade.GetDescription());
            tempDataFields.UpdateIcon(upgrade.GetIcon());
            //tempDataFields.gameObject.GetComponent<UI_WindowSizeCalculator>().CalculateAndSetHeight();
            //textField = tempGO.GetComponentInChildren<TMP_Text>();
            //textField.text = upgrade.GetDescription();
            //newHeight = tempDataFields.label.preferredHeight;

            //tempTransform = tempDataFields.GetComponent<RectTransform>();
            //newHeight = tempTransform.sizeDelta.y;
            // tempTransform.sizeDelta = new Vector2(tempTransform.rect.width, newHeight);
            //size += newHeight;
        }
        //size += bonusesLayout.padding.bottom + bonusesLayout.padding.top;
        //bonusesParent.sizeDelta = new Vector2(bonusesParent.rect.width, size);
    }
    void ResizeWindow()
    {
        float size = 0;
        size += 70;
        size += damageParent.rect.height;
        size += simpleDataParent.rect.height;
        size += bonusesParent.rect.height;
        //Debug.Log("damage height = " + damageParent.rect.height);
        //Debug.Log("simple height = " + simpleDataParent.rect.height);
        //Debug.Log("bonuses height = " + bonusesParent.rect.height);
        windowTransform.sizeDelta = new Vector2(windowTransform.rect.width, size);
    }

    void HideWindow()
    {
        window.SetActive(false);
    }
    void ShowWindow()
    {
        window.SetActive(true);
    }
}
