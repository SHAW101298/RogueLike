using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum ENUM_TriggerType
{
    semi,
    auto
}
public enum ENUM_DamageType
{
    Heat,
    Ice,
    Toxin,
    Electricity,
    Chaos,
    Piercing
}
[System.Serializable]
public class GunDamageMultipliers
{
    public float[] multipliers;

    public GunDamageMultipliers()
    {
        int size = (int)ENUM_DamageType.Piercing + 1;
        multipliers = new float[size];
        for(int i = 0; i < size; i++)
        {
            multipliers[i] = 1;
        }
    }
}

[System.Serializable]
public class GunDamageData
{
    public int damage;
    public ENUM_DamageType damageType;
}
[System.Serializable]
public class GunStats
{
    public List<GunDamageData> damageArray;
    public GunDamageMultipliers damageMultipliersOnAffliction;
    public float totalDamageMultiplier = 1;
    public float critChance;
    public float critMultiplier;
    public float afflictionChance;
    public int magazineMax;
    public int ammoMax;
    public float reloadTime;
    public float timeBetweenShots;
    [Space(10)]
    public ENUM_TriggerType triggerType;
    [UnityEngine.Tooltip("40 Speed is Neutral")]
    public float projectileSpeed;
    public int punchThrough;
}
