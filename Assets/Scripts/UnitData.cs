using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_Faction
{
    player,
    enemy,
    boss
}
public abstract class UnitData : MonoBehaviour
{
    public ENUM_Faction faction;
}
