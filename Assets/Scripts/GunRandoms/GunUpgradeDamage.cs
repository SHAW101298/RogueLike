using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunUpgradeDamage : GunUpgradeBase
{
    public ENUM_DamageType damageType;
    public int flatAmount = 0;
    public float percentageAmount = 1;

    [Header("Min-Max Amount")]
    public int flatMin = 5;
    public int flatMax = 25;
    public float percentageMin = 20;
    public float percentageMax = 100;


    public GunUpgradeDamage()
    {
        upgradeType = ENUM_GunRandomUpgradeType.damage;
    }

    public override void Roll()
    {
        int randInt = Random.Range(0, 2);
        float randFloat;
        
        if(randInt == 0)
        {
            randFloat = Random.Range(flatMin, flatMax);
            flatAmount = (int)randFloat;
        }
        else
        {
            randFloat = Random.Range(percentageMin, percentageMax);
            //percentageAmount = randFloat;
            percentageAmount = Mathf.Round(randFloat);
            percentageAmount /= 100;
        }
        int damageTypeMax = (int)ENUM_DamageType.Physical;
        int randDamageType = Random.Range(0,1 + damageTypeMax);
        damageType = (ENUM_DamageType)randDamageType;
    }

    public override string ToString()
    {
        string tekst = "Damage type = " + damageType.ToString() + " | Flat = " + flatAmount + " | Percentage = " + percentageAmount;
        return tekst;
    }
}
