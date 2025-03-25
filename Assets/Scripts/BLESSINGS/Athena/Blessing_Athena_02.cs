using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Blessing_Athena_02 : Blessing_Base
{
    [SerializeField] float elementalDamageIncrease;

    public override void Apply()
    {
        Debug.Log("Activating " + title);
        GetParent();
        player.bonusStats.elementalDamageModifier.AddDataToAll(elementalDamageIncrease);
    }

    public override string GetDescription()
    {
        string text = "Increase damage from elements by " + elementalDamageIncrease*100 + "%.";
        return text;
    }

    public override void Remove()
    {
        Debug.Log("Deactivating " + title);
        player.bonusStats.elementalDamageModifier.SubstractDataFromAll(elementalDamageIncrease);
    }


}
