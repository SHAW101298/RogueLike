using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_MovementSpeedIncreaseFlat : Effect_Base
{
    public float value;
    public override void Apply()
    {
        UnitData unit = GetComponentInParent<UnitData>();
        unit.finalStats.moveSpeed += value;
    }

    public override void Refresh()
    {
        timer = remainingTime;
    }

    public override void Remove()
    {
        UnitData unit = GetComponentInParent<UnitData>();
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
        EffectLogic();
    }

}
