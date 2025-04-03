using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SuperiorAim : Effect_Base
{
    public float timeBeforeRemoval;
    public int maxStacks;
    //float timer;
    int stacks;

    public override void Apply()
    {
        throw new System.NotImplementedException();
    }

    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }

    public override void Remove()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        EffectLogic();
    }
    public void EffectLogic()
    {
        timer += Time.deltaTime;
        if(timer >= timeBeforeRemoval)
        {
            timer = 0;
        }
    }
    public void AddStack()
    {

    }
}
