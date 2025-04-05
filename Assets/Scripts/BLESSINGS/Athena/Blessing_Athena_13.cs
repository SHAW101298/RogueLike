using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_13 : Blessing_Base
{
    // Zwiêkszenie szansy na cios krytyczny po zadaniu ciosu krytycznego
    [SerializeField] float criticalChanceIncrease;
    [SerializeField] float blessingTime;
    float timer;
    bool blessingActive;
    public override void Apply()
    {
        GetParent();
        player.events.OnCriticalHitEvent.AddListener(BlessingLogicOnCriticalHit);
    }

    public override string GetDescription()
    {
        string text = "After a critical hit, increase critical chance by " + criticalChanceIncrease + "% for " + blessingTime + "seconds.";
        return text;
    }

    public override void Remove()
    {
        TakeOff();
        player.events.OnCriticalHitEvent.RemoveListener(BlessingLogicOnCriticalHit);
    }
    public void BlessingLogicOnCriticalHit()
    {
        if(blessingActive == true)
        {
            timer = 0;
            return;
        }
        blessingActive = true;
        player.bonusStats.criticalChanceModifier += criticalChanceIncrease;
    }
    public void TakeOff()
    {
        if(blessingActive == true)
        {
            player.bonusStats.criticalChanceModifier -= criticalChanceIncrease;
            blessingActive = false;
            timer = 0;
        }
    }

    private void Update()
    {
        if(blessingActive == true)
        {
            timer += Time.deltaTime;
            if(timer >= blessingTime)
            {
                TakeOff();
            }
        }
    }
}