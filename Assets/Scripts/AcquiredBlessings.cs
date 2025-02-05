using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquiredBlessings : MonoBehaviour
{
    public UnitData data;
    public List<Blessing_Base> list;
    public GameObject blessingsParent;

    public void AddBlessing(Blessing_Base blessing)
    {
        list.Add(blessing);
        blessing.transform.SetParent(blessingsParent.transform);
        blessing.Apply();
        data.RecalculateStats();
    }
    public void RecalculateStats()
    {

    }
}
