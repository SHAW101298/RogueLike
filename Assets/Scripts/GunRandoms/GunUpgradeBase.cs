using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_GunRandomUpgradeType
{
    ammoMax,
    magazineSize,
    damage,
    punchThrough,
    reloadSpeed
}
[System.Serializable]
public class GunUpgradeBase
{
    public ENUM_GunRandomUpgradeType upgradeType;
    public virtual void Roll()
    {

    }
}
