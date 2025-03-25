using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Blessing_Athena_02 : Blessing_Base
{
    [SerializeField] float elementalDamageIncrease;

    public override void Apply()
    {
        player = GetComponentInParent<PlayerData>();
        player.stats.globalElementalDamageModifier += elementalDamageIncrease;
    }

    public override string GetDescription()
    {
        string text = "Increase damage from elements by " + elementalDamageIncrease*100 + "%.";
        return text;
    }

    public override void Remove()
    {
        player.stats.globalElementalDamageModifier -= elementalDamageIncrease;
    }


}
