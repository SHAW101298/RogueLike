using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_DamageIncrease : Effect_Base
{
    public float value;
    public override void Apply()
    {
        UnitData unit = GetComponentInParent<UnitData>();
        isActive = true;
    }

    public override void Refresh()
    {
        timer = remainingTime;
    }

    public override void Remove()
    {
        UnitData unit = GetComponentInParent<UnitData>();
        isActive = false;
        unit.effects.RemoveEffect(this);
        unit.effects.RecalculateStats();
    }
    void EffectLogic()
    {
        // EFFECT IS PERMANENT
        if (isPermanent == true)
            return;

        timer -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            Remove();
        }

    }
    private void Update()
    {
        if (isActive == false)
            return;
        EffectLogic();
    }
}
