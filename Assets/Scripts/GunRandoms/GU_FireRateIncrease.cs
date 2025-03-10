using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_FireRateIncrease : GunUpgradeBase
{
    [SerializeField] float[] timeBetweenShots;
    public override void Apply(Gun gun)
    {
        gun.modifiedStats.timeBetweenShots -= timeBetweenShots[upgradeLevel];
    }

    public override string GetDescription()
    {
        return "Increase firerate by " + timeBetweenShots[upgradeLevel] * 100;
    }

    public override string GetTextValue()
    {
        return timeBetweenShots[upgradeLevel].ToString();
    }

    public override void Remove(Gun gun)
    {
        gun.modifiedStats.timeBetweenShots += timeBetweenShots[upgradeLevel];
    }

}
