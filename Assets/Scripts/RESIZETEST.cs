using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RESIZETEST : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] RectTransform damageTransform;
    [SerializeField] RectTransform statsTransform;
    [SerializeField] GameObject damagePrefab;
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
}
