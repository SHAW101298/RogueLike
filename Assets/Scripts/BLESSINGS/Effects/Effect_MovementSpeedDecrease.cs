using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_MovementSpeedDecrease : Effect_Base
{
    public float value;

    public override void Apply()
    {
        
    }
    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }
    public override void Remove()
    {
        throw new System.NotImplementedException();
    }

    void EffectLogic()
    {
        // EFFECT IS PERMANENT
        if (isPermanent == true)
            return;

        timer -= Time.deltaTime;

        if(remainingTime <= 0)
        {
            Remove();
        }

    }
    private void Update()
    {
        EffectLogic
    }
}
