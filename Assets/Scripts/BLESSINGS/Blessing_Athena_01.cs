using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Blessing_Athena_01 : Blessing_Base
{
    [SerializeField] float criticalChanceIncrease;
    [SerializeField] float afflictionChanceIncrease;
    public override void Apply()
    {
        player = GetComponentInParent<PlayerData>();
        player.events.OnShieldDepleted.AddListener(BlessingLogic);
        player.events.OnShieldRegenerationStart.AddListener(BlessingLogic2);
    }

    public override string GetDescription()
    {
        string text = "When shield amount is 0, increase critial chance by " + criticalChanceIncrease + "% and affliction chance by " + afflictionChanceIncrease + "%.";
        return text;
    }

    public override void Remove()
    {
        player.events.OnShieldDepleted.RemoveListener(BlessingLogic);
        player.events.OnShieldRegenerationStart.RemoveListener(BlessingLogic2);
    }

    void BlessingLogic()
    {
        player.stats.globalCriticalChanceModifier += criticalChanceIncrease;
        player.stats.globalAfflictionChanceModifier += afflictionChanceIncrease;
    }
    void BlessingLogic2()
    {
        player.stats.globalCriticalChanceModifier -= criticalChanceIncrease;
        player.stats.globalAfflictionChanceModifier -= afflictionChanceIncrease;
    }
}
