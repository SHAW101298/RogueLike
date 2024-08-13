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
public class GunDamageData
{
    public int damage;
    public ENUM_DamageType damageType;
}
[System.Serializable]
public class GunStats
{
    public List<GunDamageData> damageArray;
    public ElementalTable damageMultipliersOnAffliction;
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

    public GunStats()
    {
        damageArray = new List<GunDamageData>();
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
            GunDamageData data = new GunDamageData();
            data.damage = donor.damageArray[i].damage;
            data.damageType = donor.damageArray[i].damageType;
            damageArray.Add(data);
        }

        for(int i = 0; i < damageMultipliersOnAffliction.Count(); i++)
        {
            damageMultipliersOnAffliction.SetData(i, donor.damageMultipliersOnAffliction.GetData(i));
        }

        totalDamageMultiplier = donor.totalDamageMultiplier;
        critChance = donor.critChance;
        critMultiplier = donor.critMultiplier;
        afflictionChance = donor.afflictionChance;
        magazineMax = donor.magazineMax;
        ammoMax = donor.ammoMax;
        reloadTime = donor.reloadTime;
        timeBetweenShots = donor.timeBetweenShots;
        triggerType = donor.triggerType;
        projectileSpeed = donor.projectileSpeed;
        punchThrough = donor.punchThrough;
    }
}
