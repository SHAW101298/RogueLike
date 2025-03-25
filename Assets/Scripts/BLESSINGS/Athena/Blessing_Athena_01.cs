using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Blessing_Athena_01 : Blessing_Base
{
    [SerializeField] float criticalChanceIncrease;
    [SerializeField] float afflictionChanceIncrease;
    bool activeBlessing = false;
    public override void Apply()
    {
        GetParent();
        player.events.OnShieldDepleted.AddListener(BlessingLogicDepleted);
        player.events.OnShieldRegenerationStart.AddListener(BlessingLogicRestoration);

        // Check if max shield is 0
        if (player.finalStats.shieldMax == 0)
        {
            BlessingLogicDepleted();
        }
    }

    public override string GetDescription()
    {
        string text = "When shield amount is 0, increase critial chance by " + criticalChanceIncrease + "% and affliction chance by " + afflictionChanceIncrease + "%.";
        return text;
    }

    public override void Remove()
    {
        if(activeBlessing == true)
        {
            BlessingLogicRestoration();
        }
        player.events.OnShieldDepleted.RemoveListener(BlessingLogicDepleted);
        player.events.OnShieldRegenerationStart.RemoveListener(BlessingLogicRestoration);
    }

    void BlessingLogicDepleted()
    {
        if (activeBlessing == true)
        {
            return;
        }
        player.bonusStats.criticalChanceModifier += criticalChanceIncrease;
        player.bonusStats.afflictionChanceModifier += afflictionChanceIncrease;
        activeBlessing = true;
    }
    void BlessingLogicRestoration()
    {
        if(activeBlessing == false)
        {
            return;
        }

        player.bonusStats.criticalChanceModifier -= criticalChanceIncrease;
        player.bonusStats.afflictionChanceModifier -= afflictionChanceIncrease;
        activeBlessing = false;
    }
}
