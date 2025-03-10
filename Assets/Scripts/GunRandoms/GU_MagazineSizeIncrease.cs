using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_MagazineSizeIncrease : GunUpgradeBase
{
    [SerializeField] int[] magazineSize;
    public override void Apply(Gun gun)
    {
        gun.modifiedStats.magazineMax += magazineSize[upgradeLevel];
    }

    public override string GetDescription()
    {
        return "Increase gun magazine size by  " + magazineSize[upgradeLevel];
    }

    public override string GetTextValue()
    {
        return magazineSize[upgradeLevel].ToString();
    }

    public override void Remove(Gun gun)
    {
        gun.modifiedStats.magazineMax -= magazineSize[upgradeLevel];
    }

}
