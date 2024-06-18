using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgradePunchThrough : GunUpgradeBase
{
    public int flatAmount;

    [Header("Min-Max Amount")]
    public int flatMin;
    public int flatMax;
    public override void Roll()
    {
        flatAmount = Random.Range(flatMin, 1 + flatMax);
    }

    public GunUpgradePunchThrough()
    {
        upgradeType = ENUM_GunRandomUpgradeType.punchThrough;
    }
}
