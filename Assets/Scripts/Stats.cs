using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats:MonoBehaviour
{
    public float healthMax;
    public float health;
    public float healthRegen;
    public float healthDelay;

    public float staminaMax;
    public float stamina;
    public float staminaRegen;
    public float staminaDelay;

    public float shieldMax;
    public float shield;
    public float shieldRegen;
    public float shieldDelay;

    public float moveSpeed;

    //public float[] flatResistances;
    //public float[] percentResistances;
    [Header("Resistance")]
    public float percentDamageResistance;
    public ElementalTable flatElementalResistance;
    public float flatNonElementalResistance;
    public ElementalTable percentElementalResistance;
    public float percentNonElementalResistance;
    [Header("Damage")]
    public ElementalTable elementalDamageModifier;
    public float nonElementalDamageModifier;
    public float totalElementalDamageModifier;
    public float damageModifier;
    public float gunDamageModifier;
    public float abilityDamageModifier;
    public float criticalChanceModifier;
    public float criticalDamageModifier;
    public float afflictionChanceModifier;
    

    public float GetFlatResist(ENUM_DamageType type)
    {
        return flatElementalResistance.GetData(type);
    }
    public float GetPercentResist(ENUM_DamageType type)
    {
        return percentElementalResistance.GetData(type);
    }
    /*
    public float GetFlatResist(ENUM_DamageType type)
    {
        return flatResistances[(int)type];
    }
    public float GetPercentResist(ENUM_DamageType type)
    {
        return percentResistances[(int)type];
    }
    */

    void CreateResistanceTables()
    {
        flatElementalResistance = new ElementalTable();
        percentElementalResistance = new ElementalTable();
        //int size = (int)ENUM_DamageType.Piercing + 1;
        //flatResistances = new float[size];
        //percentResistances = new float[size];
    }

    public void CopyValues(Stats stats1)
    {
        healthMax = stats1.healthMax;
        healthRegen = stats1.healthRegen;
        healthDelay = stats1.healthDelay;
        staminaMax = stats1.staminaMax;
        staminaRegen = stats1.staminaRegen;
        staminaDelay = stats1.staminaDelay;
        shieldMax = stats1.shieldMax;
        shieldRegen = stats1.shieldRegen;
        shieldDelay = stats1.shieldDelay;

        moveSpeed = stats1.moveSpeed;

        percentDamageResistance = stats1.percentDamageResistance;

        flatElementalResistance.SetData(stats1.flatElementalResistance);
        percentElementalResistance.SetData(stats1.percentElementalResistance);

        flatNonElementalResistance = stats1.flatNonElementalResistance;
        percentNonElementalResistance = stats1.flatNonElementalResistance;
        elementalDamageModifier.SetData(stats1.elementalDamageModifier);
        nonElementalDamageModifier = stats1.nonElementalDamageModifier;
        totalElementalDamageModifier = stats1.totalElementalDamageModifier;
        damageModifier = stats1.damageModifier;
        gunDamageModifier = stats1.gunDamageModifier;
        abilityDamageModifier = stats1.abilityDamageModifier;
        criticalChanceModifier = stats1.criticalChanceModifier;
        criticalDamageModifier = stats1.criticalDamageModifier;
        afflictionChanceModifier = stats1.afflictionChanceModifier;
    /*
    for(int i = 0; i < 6; i++)
    {
        flatResistances[i] = stats1.flatResistances[i];
        percentResistances[i] = stats1.percentResistances[i];
    }
    */
}
    public void CombineStats(Stats stats1, Stats stats2)
    {
        healthMax = stats1.healthMax + stats2.healthMax;
        healthRegen = stats1.healthRegen + stats2.healthRegen;
        healthDelay = stats1.healthDelay + stats2.healthDelay;
        staminaMax = stats1.staminaMax + stats2.staminaMax;
        staminaRegen = stats1.staminaRegen + stats2.staminaRegen;
        staminaDelay = stats1.staminaDelay + stats2.staminaDelay;
        shieldMax = stats1.shieldMax + stats2.shieldMax;
        shieldRegen = stats1.shieldRegen + stats2.shieldRegen;
        shieldDelay = stats1.shieldDelay + stats2.shieldDelay;

        moveSpeed = stats1.moveSpeed + stats2.moveSpeed;
        percentDamageResistance = stats1.percentDamageResistance + stats2.percentDamageResistance;

        flatElementalResistance.AddData(stats1.flatElementalResistance);
        flatElementalResistance.AddData(stats2.flatElementalResistance);

        percentElementalResistance.AddData(stats1.percentElementalResistance);
        percentElementalResistance.AddData(stats2.percentElementalResistance);


        flatNonElementalResistance = stats1.flatNonElementalResistance + stats2.flatNonElementalResistance;
        percentNonElementalResistance = stats1.flatNonElementalResistance + stats2.flatNonElementalResistance;
        elementalDamageModifier.AddData(stats1.elementalDamageModifier);
        elementalDamageModifier.AddData(stats2.elementalDamageModifier);

        nonElementalDamageModifier = stats1.nonElementalDamageModifier + stats2.nonElementalDamageModifier;
        totalElementalDamageModifier = stats1.totalElementalDamageModifier + stats2.totalElementalDamageModifier;
        damageModifier = stats1.damageModifier + stats2.damageModifier;
        gunDamageModifier = stats1.gunDamageModifier + stats2.gunDamageModifier;
        abilityDamageModifier = stats1.abilityDamageModifier + stats2.abilityDamageModifier;
        criticalChanceModifier = stats1.criticalChanceModifier + stats2.criticalChanceModifier;
        criticalDamageModifier = stats1.criticalDamageModifier + stats2.criticalDamageModifier;
        afflictionChanceModifier = stats1.afflictionChanceModifier + stats2.afflictionChanceModifier;

        /*
        for (int i = 0; i < 6; i++)
        {
            flatResistances[i] = stats1.flatResistances[i] + stats2.flatResistances[i];
            percentResistances[i] = stats1.percentResistances[i] + stats2.percentResistances[i];
        }
        */
    }

    public Stats()
    {
        CreateResistanceTables();
    }
}
