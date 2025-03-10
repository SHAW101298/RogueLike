using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_ProjectileAmountIncrease : GunUpgradeBase
{
    [SerializeField] int[] projectileCount;
    public override void Apply(Gun gun)
    {
        gun.modifiedStats.numberOfProjectiles += projectileCount[upgradeLevel];
    }

    public override string GetDescription()
    {
        return "Increase amount of projectiles by " + projectileCount[upgradeLevel];
    }

    public override string GetTextValue()
    {
        return projectileCount[upgradeLevel].ToString();
    }

    public override void Remove(Gun gun)
    {
        gun.modifiedStats.numberOfProjectiles -= projectileCount[upgradeLevel];
    }

}
