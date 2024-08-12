using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afflictions : MonoBehaviour
{
    public Affliction[] afflictions;

    private void Awake()
    {
    }

    public bool ReturnAfflictionState(ENUM_DamageType damageType)
    {
        return afflictions[(int)damageType].afflictionActive;
    }

    void CreateAfflictions()
    {
        int size = (int)ENUM_DamageType.Piercing + 1;
        afflictions = new Affliction[size];
        Affliction tempAffliction;
        for (int i = 0; i < (int)ENUM_DamageType.Piercing; i++)
        {
            tempAffliction = new Affliction();
            tempAffliction.type = (ENUM_DamageType)i;
            afflictions[i] = tempAffliction;
        }
    }
    public Afflictions()
    {
        CreateAfflictions();
    }
}
[System.Serializable]
public class Affliction
{
    public ENUM_DamageType type;
    public bool afflictionActive;
    public float remainingTime;
}
