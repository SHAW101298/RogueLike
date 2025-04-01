using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afflictions : MonoBehaviour
{
    public Affliction[] afflictions;

    private void Update()
    {
        LoopThroughAfflictionsTimers();
    }
    void LoopThroughAfflictionsTimers()
    {
        // Loop through all afflictions
        for (int i = 0; i < afflictions.Length; i++)
        {
            // If affliction is active
            if (afflictions[i].afflictionActive == true)
            {
                // Affliction time remaining
                afflictions[i].remainingTime -= Time.deltaTime;
                if (afflictions[i].remainingTime <= 0)
                {
                    afflictions[i].afflictionActive = false;
                    afflictions[i].remainingTime = 0;
                }
            }
        }
    }

    public bool ReturnAfflictionState(ENUM_DamageType damageType)
    {
        return afflictions[(int)damageType].afflictionActive;
    }
    public void ApplyAfflicion(ENUM_DamageType damageType)
    {
        afflictions[(int)damageType].afflictionActive = true;
        afflictions[(int)damageType].remainingTime = 10;
    }

    void CreateAfflictions()
    {
        int size = (int)ENUM_DamageType.Piercing + 1;
        afflictions = new Affliction[size];
        Affliction tempAffliction;
        for (int i = 0; i < size; i++)
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
[System.Serializable]
public class ElementalTable
{
    [SerializeField] float[] data;
    public int Count()
    {
        return data.Length;
    }

    public void SetData(int i, float val)
    {
        data[i] = val;
    }
    public void SetData(ElementalTable table)
    {
        int x = data.Length;
        for(int i = 0; i < x; i++)
        {
            data[i] = table.data[i];
        }
    }
    public void SetData(ENUM_DamageType type, float val)
    {
        data[(int)type] = val;
    }
    public void AddData(ENUM_DamageType type, float val)
    {
        data[(int)type] += val;
    }
    public void AddData(ElementalTable table)
    {
        int x = data.Length;
        for(int i = 0; i < x; i++)
        {
            data[i] += table.data[i];
        }
    }
    public void AddDataToAll(float val)
    {
        int x = data.Length;
        for (int i = 0; i < x; i++)
        {
            data[i] += val;
        }
    }
    public void SubstractData(ENUM_DamageType type, float val)
    {
        data[(int)type] -= val;
    }
    public void SubstractDataFromAll(float val)
    {
        int x = data.Length;
        for (int i = 0; i < x; i++)
        {
            data[i] -= val;
        }
    }
    public float GetData(int i)
    {
        return data[i];
    }
    public float GetData(ENUM_DamageType type)
    {
        return data[(int)type];
    }

    public ElementalTable()
    {
        int size = (int)ENUM_DamageType.Piercing + 1;
        data = new float[size];
    }

}
