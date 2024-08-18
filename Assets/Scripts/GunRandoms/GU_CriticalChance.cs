using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_CriticalChance : GunUpgradeBase
{
    private float[] criticalChanceIncrease;
    public override void Apply(Gun gun)
    {
        gun.modifiedStats.critChance += criticalChanceIncrease[upgradeLevel];
    }
    public override void Remove(Gun gun)
    {
        gun.modifiedStats.critChance -= criticalChanceIncrease[upgradeLevel];
    }

    public override string GetDescription()
    {
        return "Increase gun critical hit chance by " + criticalChanceIncrease[upgradeLevel] + "%";
    }

    public override string GetTextValue()
    {
        return criticalChanceIncrease[upgradeLevel].ToString();
    }


    public GU_CriticalChance()
    {
        criticalChanceIncrease = new float[5];
        criticalChanceIncrease[0] = 10;
        criticalChanceIncrease[1] = 20;
        criticalChanceIncrease[2] = 30;
        criticalChanceIncrease[3] = 40;
        criticalChanceIncrease[4] = 60;
    }
}
