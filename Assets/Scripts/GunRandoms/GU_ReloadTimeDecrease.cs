using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_ReloadTimeDecrease : GunUpgradeBase
{
    public float[] reloadTime;

    public override void Apply(Gun gun)
    {
        gun.modifiedStats.reloadTime -= reloadTime[upgradeLevel];
    }
    public override void Remove(Gun gun)
    {
        gun.modifiedStats.reloadTime -= reloadTime[upgradeLevel];
    }
    public override string GetDescription()
    {
        return "Decrease reload time by " + reloadTime[upgradeLevel] + " seconds.";
    }
    public override string GetTextValue()
    {
        return reloadTime[upgradeLevel].ToString();
    }
}
