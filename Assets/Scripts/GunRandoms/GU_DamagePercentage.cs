using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GU_DamagePercentage : GunUpgradeBase
{
    public float[] damageIncreaseOnLevels;
    public int upgradeLevel;

    public override void Apply(Gun gun)
    {
        gun.modifiedStats.totalDamageMultiplier += damageIncreaseOnLevels[upgradeLevel];
    }
    public override void Remove(Gun gun)
    {
        gun.modifiedStats.totalDamageMultiplier -= damageIncreaseOnLevels[upgradeLevel];
    }
    public string GetDescription()
    {
        return "Increase gun damage by %";
    }
    public string GetTextValue()
    {
        return damageIncreaseOnLevels[upgradeLevel].ToString();
    }
    public GU_DamagePercentage()
    {
        damageIncreaseOnLevels = new float[5];
        damageIncreaseOnLevels[0] = 20;
        damageIncreaseOnLevels[1] = 40;
        damageIncreaseOnLevels[2] = 60;
        damageIncreaseOnLevels[3] = 80;
        damageIncreaseOnLevels[4] = 100;
    }

}
