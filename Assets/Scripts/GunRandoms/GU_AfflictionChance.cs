using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GU_AfflictionChance : GunUpgradeBase
{
    [SerializeField] private float[] afflictionChanceIncrease;

    public override void Apply(Gun gun)
    {
        gun.modifiedStats.afflictionChance += afflictionChanceIncrease[upgradeLevel];
    }
    public override void Remove(Gun gun)
    {
        gun.modifiedStats.afflictionChance -= afflictionChanceIncrease[upgradeLevel];
    }

    public override string GetDescription()
    {
        return "Increases the chance of applying affliction by " + afflictionChanceIncrease[upgradeLevel] + "%";
    }

    public override string GetTextValue()
    {
        return afflictionChanceIncrease[upgradeLevel].ToString();
    }
    public GU_AfflictionChance()
    {
        afflictionChanceIncrease = new float[5];
        afflictionChanceIncrease[0] = 5;
        afflictionChanceIncrease[1] = 10;
        afflictionChanceIncrease[2] = 15;
        afflictionChanceIncrease[3] = 20;
        afflictionChanceIncrease[4] = 30;
    }
}
