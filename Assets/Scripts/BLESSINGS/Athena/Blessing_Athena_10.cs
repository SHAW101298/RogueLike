using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_10 : Blessing_Base
{
    // Po na³o¿eniu statusu, szansa na na³o¿enie dodatkowego losowego statusu
    [SerializeField] float afflictionChance;
    [SerializeField] float cooldown;
    float timer;
    bool regenerating;
    public override void Apply()
    {
        GetParent();
        player.events.OnAfflictionAppliedEvent.AddListener(BlessingLogicOnAfflictionApplied);
    }

    public override string GetDescription()
    {
        string text = "When applying affliction, there is " + afflictionChance + "% chance to apply another random affliction.";
        return text;
    }

    public override void Remove()
    {
        player.events.OnAbilityUseEvent.RemoveListener(BlessingLogicOnAfflictionApplied);
    }
    public void BlessingLogicOnAfflictionApplied()
    {

    }
}
