using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_CriticalMultiplier : GunUpgradeBase
{
    private float[] criticalMultiplierIncrease;

    public override void Apply(Gun gun)
    {
        gun.modifiedStats.critMultiplier += criticalMultiplierIncrease[upgradeLevel];
    }
    public override void Remove(Gun gun)
    {
        gun.modifiedStats.critMultiplier -= criticalMultiplierIncrease[upgradeLevel];
    }

    public override string GetDescription()
    {
        return "Increase damage done by critical hits by " + criticalMultiplierIncrease[upgradeLevel]*100 + "%";
    }

    public override string GetTextValue()
    {
        return criticalMultiplierIncrease[upgradeLevel].ToString();
    }
    public GU_CriticalMultiplier()
    {
        criticalMultiplierIncrease = new float[5];

        criticalMultiplierIncrease[0] = 0.5f;
        criticalMultiplierIncrease[1] = 1;
        criticalMultiplierIncrease[2] = 1.5f;
        criticalMultiplierIncrease[3] = 2;
        criticalMultiplierIncrease[4] = 3;
    }

}
