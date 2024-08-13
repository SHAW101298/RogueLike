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
public abstract class GunUpgradeBase : MonoBehaviour
{
    public ENUM_GunRandomUpgradeType upgradeType;
    public abstract void Apply(Gun gun);
    public abstract void Remove(Gun gun);


}
