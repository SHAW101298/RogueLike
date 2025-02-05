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
public class DamageData
{
    public int damage;
    public ENUM_DamageType damageType;
}
[System.Serializable]
public class GunStats
{
    public List<DamageData> damageArray;
    public ElementalTable damageMultipliersOnAffliction;
    [Space(20)]
    public float totalDamageMultiplier = 1;
    public float critChance;
    public float critMultiplier;
    public float afflictionChance;
    [Space(20)]
    public int numberOfProjectiles;
    [Tooltip("Size of CrossHair")]
    public float accuracy;
    [Tooltip("Amount of backlash from firing")]
    public float stability;
    public int magazineMax;
    public float reloadTime;
    public float timeBetweenShots;
    [Space(10)]
    public ENUM_TriggerType triggerType;
    [UnityEngine.Tooltip("40 Speed is Neutral")]
    public float projectileSpeed;
    public int punchThrough;

    public GunStats()
    {
        damageArray = new List<DamageData>();
        damageMultipliersOnAffliction = new ElementalTable();
        for(int i = 0; i < damageMultipliersOnAffliction.Count(); i++)
        {
            damageMultipliersOnAffliction.SetData(i, 1);
        }
    }

    public void CopyDataFrom(GunStats donor)
    {
        damageArray.Clear();
        for(int i = 0; i < donor.damageArray.Count; i++)
        {
            DamageData data = new DamageData();
            data.damage = donor.damageArray[i].damage;
            data.damageType = donor.damageArray[i].damageType;
            damageArray.Add(data);
        }

        for(int i = 0; i < damageMultipliersOnAffliction.Count(); i++)
        {
            damageMultipliersOnAffliction.SetData(i, donor.damageMultipliersOnAffliction.GetData(i));
        }

        numberOfProjectiles = donor.numberOfProjectiles;
        totalDamageMultiplier = donor.totalDamageMultiplier;
        critChance = donor.critChance;
        critMultiplier = donor.critMultiplier;
        afflictionChance = donor.afflictionChance;
        magazineMax = donor.magazineMax;
        //ammoMax = donor.ammoMax;
        reloadTime = donor.reloadTime;
        timeBetweenShots = donor.timeBetweenShots;
        triggerType = donor.triggerType;
        projectileSpeed = donor.projectileSpeed;
        punchThrough = donor.punchThrough;
        accuracy = donor.accuracy;
        stability = donor.stability;
    }
}
