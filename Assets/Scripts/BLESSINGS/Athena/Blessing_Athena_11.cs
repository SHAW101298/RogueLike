using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_11 : Blessing_Base
{
    // Zwiêkszenie szansy na na³o¿enie statusu, po na³o¿eniu statusu
    [SerializeField] float afflictionChanceIncreasePercent;
    [SerializeField] float blessingTime;
    bool blessingActive;
    float timer;
    public override void Apply()
    {
        GetParent();
        player.events.OnAfflictionAppliedEvent.AddListener(BlessingLogicOnAfflictionApplied);
    }

    public override string GetDescription()
    {
        string text = "When applying affliction increase chance of applying Afflictions by " + afflictionChanceIncreasePercent + "% for " + blessingTime + " seconds.";
        return text;
    }

    public override void Remove()
    {
        TakeOff();
        player.events.OnAfflictionAppliedEvent.RemoveListener(BlessingLogicOnAfflictionApplied);
    }
    public void BlessingLogicOnAfflictionApplied()
    {
        if(blessingActive == true)
        {
            timer = 0;
            return;
        }
        player.bonusStats.afflictionChanceModifier += afflictionChanceIncreasePercent;
        timer = 0;
        blessingActive = true;
    }
    public void TakeOff()
    {
        if(blessingActive == true)
        {
            player.bonusStats.afflictionChanceModifier -= afflictionChanceIncreasePercent;
            blessingActive = false;
        }
    }

    private void Update()
    {
        if(blessingActive == true)
        {
            timer += Time.deltaTime;
            if(timer >=  blessingTime)
            {
                timer = 0;
                TakeOff();
            }
        }
    }
}
