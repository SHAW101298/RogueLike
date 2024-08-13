using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RESIZETEST : MonoBehaviour
{
    public static RESIZETEST ins;
    private void Awake()
    {
        ins = this;
    }
    [SerializeField] GameObject window;
    [SerializeField] RectTransform damageTransform;
    [SerializeField] RectTransform bonusesTransform;
    [SerializeField] GameObject damagePrefab;
    [SerializeField] GameObject bonusPrefab;
    [Header("Fields")]
    [SerializeField] TMP_Text critChanceField;
    [SerializeField] TMP_Text critMultiplierField;
    [SerializeField] TMP_Text statusChanceField;
    [SerializeField] TMP_Text ammoField;
    [SerializeField] TMP_Text rateOfFireField;
    [SerializeField] TMP_Text stabilityField;


    [SerializeField] bool DebugRun;
    [SerializeField] Gun testGun;
    /*
     Kazde pole jest wysokie na 25 

     */

    private void Start()
    {
        testGun = GunManager.Instance.CreateGun(0);
    }
    // Update is called once per frame
    void Update()
    {
        if(DebugRun == true)
        {
            DebugRun = false;
            Foo();
        }
    }
    void Foo()
    {
        window.SetActive(false);
        GameObject tempGO;
        float size = 0;

        foreach(Transform child  in damageTransform.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(GunDamageData dmgData in testGun.modifiedStats.damageArray)
        {
            tempGO = Instantiate(damagePrefab);
            tempGO.transform.SetParent(damageTransform);
            tempGO.transform.localScale = Vector3.one;
            size += 25;
            Transform child = tempGO.transform.Find("Label");
            child.GetComponent<TMP_Text>().text = dmgData.damageType.ToString();
            child = tempGO.transform.Find("Data");
            child.GetComponent<TMP_Text>().text = dmgData.damage.ToString();

        }
        size += 20; // Space for Spacer
        damageTransform.sizeDelta = new Vector2(damageTransform.rect.width, size);

        //critChanceField.text = testGun.modifiedStats.c

        window.SetActive(true);

    }
    public void ShowData(Gun gun)
    {
        // MAKE IT CHECK IF ITS THE SAME GUN
        window.SetActive(false);
        GameObject tempGO;
        float size = 0;

        foreach (Transform child in damageTransform.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (GunDamageData dmgData in gun.modifiedStats.damageArray)
        {
            tempGO = Instantiate(damagePrefab);
            tempGO.transform.SetParent(damageTransform);
            tempGO.transform.localScale = Vector3.one;
            size += 25;
            Transform child = tempGO.transform.Find("Label");
            child.GetComponent<TMP_Text>().text = dmgData.damageType.ToString();
            child = tempGO.transform.Find("Data");
            child.GetComponent<TMP_Text>().text = dmgData.damage.ToString();
        }
        size += 20; // Space for Spacer
        damageTransform.sizeDelta = new Vector2(damageTransform.rect.width, size);

        critChanceField.text = gun.modifiedStats.critChance.ToString();
        critMultiplierField.text = gun.modifiedStats.critMultiplier.ToString();
        statusChanceField.text = gun.modifiedStats.afflictionChance.ToString();
        ammoField.text = gun.modifiedStats.magazineMax.ToString() + "/" + gun.modifiedStats.ammoMax.ToString();
        ShowBonuses(gun);
        window.SetActive(true);
    }
    void ShowBonuses(Gun gun)
    {
        GameObject tempGO;
        float newHeight = 0;
        TMP_Text textField;
        RectTransform tempTransform;
        foreach (Transform child in bonusesTransform.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(GunUpgradeBase upgrade in gun.gunUpgrades)
        {
            tempGO = Instantiate(bonusPrefab);
            tempGO.transform.SetParent(bonusesTransform);
            tempGO.transform.localScale = Vector3.one;
            textField = tempGO.GetComponentInChildren<TMP_Text>();
            textField.text = upgrade.GetDescription();
            newHeight = textField.preferredHeight;
            tempTransform = textField.transform.parent.GetComponent<RectTransform>();
            tempTransform.sizeDelta = new Vector2(tempTransform.rect.width, newHeight);
        }
        bonusesTransform.sizeDelta = new Vector2(bonusesTransform.rect.width, bonusesTransform.GetComponent<VerticalLayoutGroup>().preferredHeight + newHeight);
    }
}
