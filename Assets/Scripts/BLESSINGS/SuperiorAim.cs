using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperiorAim : Blessing_Base
{
    public float critChanceIncrease;
    public int maxStacks;
    public float timeBeforeRemoval;
    float timer;
    int stacks;
    public override void Apply()
    {
        player = GetComponentInParent<PlayerData>();
        player.events.OnWeakPointHit.AddListener(BlessingLogic);
    }

    public override string GetDescription()
    {
        string text = "Each WeakSpot hit increases critical chance by " + critChanceIncrease + "% for a maximum of " + maxStacks + " stacks. Effect lasts ";
        return text;
    }

    public override void Remove()
    {
        player.events.OnWeakPointHit.RemoveListener(BlessingLogic);
    }
    public void BlessingLogic()
    {
        if (stacks >= maxStacks)
            return;
        stacks++;
        player.stats.globalCriticalChanceModifier += critChanceIncrease;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeBeforeRemoval)
        {
            timer = 0;
            player.stats.globalCriticalChanceModifier -= stacks * critChanceIncrease;
            stacks = 0;
        }
    }
}
