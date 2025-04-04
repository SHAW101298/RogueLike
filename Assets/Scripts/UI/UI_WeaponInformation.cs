using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_WeaponInformation : MonoBehaviour
{
    PlayerData player;
    [Header("Text Fields")]
    [SerializeField] UI_DataField damageField;
    [SerializeField] TMP_Text criticalChance;
    [SerializeField] TMP_Text criticalMultiplier;
    [SerializeField] TMP_Text afflictionChance;
    [SerializeField] TMP_Text magazineSize;
    [SerializeField] TMP_Text rateOfFire;
    [SerializeField] TMP_Text Stability;
    [Header("Parents")]
    [SerializeField] UI_WindowResizer complexDataWindow;
    [SerializeField] UI_WindowResizer damageInfo;
    [SerializeField] UI_WindowResizer bonusesInfo;
    [Header("Prefabs")]
    [SerializeField] GameObject damagePrefab;
    [SerializeField] GameObject bonusPrefab;

    

    public void ShowInfo(int index)
    {
        if(player == null)
        {
            player = PlayerList.Instance.GetMyPlayer(); 
        }
        if(player.gunManagement.possesedGuns.Count <= index)
        {
            gameObject.SetActive(false);
            return;
        }
        if (player.gunManagement.possesedGuns[index] == null)
        {
            gameObject.SetActive(false);
            return;
        }
            
        gameObject.SetActive(true);

        Gun gun = player.gunManagement.possesedGuns[index];
        criticalChance.text = gun.modifiedStats.critChance.ToString() + "%";
        criticalMultiplier.text ="x " +  gun.modifiedStats.critMultiplier.ToString();
        afflictionChance.text = gun.modifiedStats.afflictionChance.ToString() + "%";
        magazineSize.text = gun.modifiedStats.magazineMax.ToString();
        rateOfFire.text = (gun.modifiedStats.timeBetweenShots * 100).ToString();

        GameObject temp;
        UI_DataField tempField;

        /*
        foreach(Transform child in damageInfo.transform)
        {
            Destroy(child.gameObject);
        }
        */

        damageField.UpdateAll(gun.modifiedStats.basedamage.damageType.ToString(), gun.modifiedStats.numberOfProjectiles + "x" + gun.modifiedStats.basedamage.damage, IconsManager.Instance.GetAfflictionIcon(gun.modifiedStats.basedamage.damageType));

        /*
        for(int i = 0; i < gun.modifiedStats.damageArray.Count; i++)
        {
            temp = Instantiate(damagePrefab);
            temp.transform.SetParent(damageInfo.transform);
            temp.transform.localScale = Vector3.one;
            tempField = temp.GetComponent<UI_DataField>();
            if(gun.modifiedStats.numberOfProjectiles > 1)
            {
                //tempField.UpdateBoth(gun.modifiedStats.damageArray[i].damageType.ToString(), gun.modifiedStats.numberOfProjectiles + "x" + gun.modifiedStats.damageArray[i].damage);
                tempField.UpdateAll(gun.modifiedStats.damageArray[i].damageType.ToString(), gun.modifiedStats.numberOfProjectiles + "x" + gun.modifiedStats.damageArray[i].damage, IconsManager.Instance.GetAfflictionIcon(gun.modifiedStats.damageArray[i].damageType));
            }
            else
            {
                tempField.UpdateBoth(gun.modifiedStats.damageArray[i].damageType.ToString(), gun.modifiedStats.damageArray[i].damage);
                tempField.UpdateAll(gun.modifiedStats.damageArray[i].damageType.ToString(), gun.modifiedStats.damageArray[i].damage, IconsManager.Instance.GetAfflictionIcon(gun.modifiedStats.damageArray[i].damageType));
            }
        }
        */

        //damageInfo.Calculate();

        
        foreach (Transform child in bonusesInfo.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < gun.gunUpgrades.Count; i++)
        {
            temp = Instantiate(bonusPrefab);
            temp.transform.SetParent(bonusesInfo.transform);
            temp.transform.localScale = Vector3.one;
            tempField = temp.GetComponent<UI_DataField>();
            tempField.UpdateLabel(gun.gunUpgrades[i].GetDescription());
            tempField.UpdateIcon(gun.gunUpgrades[i].GetIcon());
        }
        /*
        bonusesInfo.Calculate();

        damageInfo.rectTransform.anchoredPosition = new Vector2(0, 0);
        Vector2 pos = new Vector2(0, -damageInfo.rectTransform.sizeDelta.y - 10);

        bonusesInfo.rectTransform.anchoredPosition = pos;
        */
        
    }

    public void ClearWindow()
    {
        criticalChance.text = "";
        criticalMultiplier.text = "";
        afflictionChance.text = "";
        magazineSize.text = "";
        rateOfFire.text = "";

        foreach (Transform child in bonusesInfo.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
