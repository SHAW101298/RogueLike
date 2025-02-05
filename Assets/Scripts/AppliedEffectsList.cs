using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppliedEffectsList : MonoBehaviour
{
    [SerializeField] UnitData data;
    [SerializeField] List<Effect_Base> list;
    [SerializeField] GameObject effectParent;

    public void AddEffect(Effect_Base effect)
    {
        effect.gameObject.transform.SetParent(effectParent.transform);
        list.Add(effect);
    }
    public void RemoveEffect(Effect_Base effect)
    {
        list.Remove(effect);
        Destroy(effect);
    }
    public void RecalculateStats()
    {
        //data.finalStats.CopyValues(data.baseStats);
        for(int i = 0; i < list.Count; i++)
        {
            list[i].Apply();
        }
    }

}
