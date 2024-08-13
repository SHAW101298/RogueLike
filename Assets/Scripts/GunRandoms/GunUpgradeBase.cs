using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GunUpgradeBase : MonoBehaviour
{
    public int upgradeLevel;
    public abstract void Apply(Gun gun);
    public abstract void Remove(Gun gun);
    public abstract string GetDescription();
    public abstract string GetTextValue();



}
