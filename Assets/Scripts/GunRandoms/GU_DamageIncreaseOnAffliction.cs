using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_DamageIncreaseOnAffliction : GunUpgradeBase
{
    private float[] damageIncreaseOnLevels;
    private ENUM_DamageType damageType;

    public override void Apply(Gun gun)
    {
        gun.modifiedStats.damageMultipliersOnAffliction.AddData(damageType, damageIncreaseOnLevels[upgradeLevel] /100);
    }
    public override void Remove(Gun gun)
    {
        gun.modifiedStats.damageMultipliersOnAffliction.AddData(damageType, damageIncreaseOnLevels[upgradeLevel]/100);
    }
    public override string GetDescription()
    {
        return "Increases gun damage on enemies under " + damageType.ToString() + " affliction by " + damageIncreaseOnLevels[upgradeLevel] + "%";
    }

    public override string GetTextValue()
    {
        return damageIncreaseOnLevels[upgradeLevel].ToString();
    }

    
    public GU_DamageIncreaseOnAffliction()
    {
        damageIncreaseOnLevels = new float[5];
        damageIncreaseOnLevels[0] = 35;
        damageIncreaseOnLevels[1] = 70;
        damageIncreaseOnLevels[2] = 110;
        damageIncreaseOnLevels[3] = 150;
        damageIncreaseOnLevels[4] = 200;
    }
}
