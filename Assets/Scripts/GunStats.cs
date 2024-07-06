using System.Collections;
using System.Collections.Generic;

public enum ENUM_TriggerType
{
    semi,
    auto
}
public enum ENUM_DamageType
{
    heat,
    ice,
    toxin,
    electricity,
    chaos,
    physical
}
[System.Serializable]
public class GunDamageData
{
    public int damage;
    public ENUM_DamageType damageType;
}
[System.Serializable]
public struct GunStats
{
    public int magazineMax;
    public int ammoMax;
    public float reloadTime;
    public float timeBetweenShots;
    public ENUM_TriggerType triggerType;
    [UnityEngine.Tooltip("40 Speed is Neutral")]
    public float projectileSpeed;
    public List<GunDamageData> damageArray;
    public int punchThrough;
}
