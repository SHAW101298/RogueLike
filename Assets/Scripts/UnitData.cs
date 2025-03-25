using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum ENUM_Faction
{
    player,
    enemy,
    boss
}
public abstract class UnitData : NetworkBehaviour
{
    public ENUM_Faction faction;
    public Stats baseStats;
    public Stats bonusStats;
    public Stats finalStats;
    public Afflictions afflictions;
    public AppliedEffectsList effects;

    public abstract void RecalculateStats();
}
