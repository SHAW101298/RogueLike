using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_ChaoticUpgrade : GunUpgradeBase
{
    public List<GunUpgradeBase> upgrades;

    public void CreateChaoticUpgrade()
    {
        GunUpgradeBase upgrade = GunUpgradeRoller.ins.GetRandomRoll();

    }

    public override void Apply(Gun gun)
    {
        foreach(GunUpgradeBase upgrade in upgrades)
        {
            upgrade.Apply(gun);
        }
    }

    public override string GetDescription()
    {
        string desc = "";
        foreach (GunUpgradeBase upgrade in upgrades)
        {
            desc += upgrade.GetDescription() + "\n";
        }
        return desc;
    }

    public override string GetTextValue()
    {
        string text = "";
        foreach (GunUpgradeBase upgrade in upgrades)
        {
            text += upgrade.GetTextValue() + "\n";
        }
        return text;
    }

    public override void Remove(Gun gun)
    {
        foreach (GunUpgradeBase upgrade in upgrades)
        {
            upgrade.Remove(gun);
        }
    }
}
