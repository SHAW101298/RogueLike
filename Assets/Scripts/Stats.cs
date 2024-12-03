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

    public float[] flatResistances;
    public float[] percentResistances;

    public float GetFlatResist(ENUM_DamageType type)
    {
        return flatResistances[(int)type];
    }
    public float GetPercentResist(ENUM_DamageType type)
    {
        return percentResistances[(int)type];
    }

    void CreateResistanceTables()
    {
        int size = (int)ENUM_DamageType.Piercing + 1;
        flatResistances = new float[size];
        percentResistances = new float[size];
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
        for(int i = 0; i < 6; i++)
        {
            flatResistances[i] = stats1.flatResistances[i];
            percentResistances[i] = stats1.percentResistances[i];
        }
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

        for (int i = 0; i < 6; i++)
        {
            flatResistances[i] = stats1.flatResistances[i] + stats2.flatResistances[i];
            percentResistances[i] = stats1.percentResistances[i] + stats2.percentResistances[i];
        }
    }

    public Stats()
    {
        CreateResistanceTables();
    }
}
