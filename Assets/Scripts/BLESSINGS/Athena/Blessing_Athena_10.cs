using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_10 : Blessing_Base
{
    // Po na�o�eniu statusu, szansa na na�o�enie dodatkowego losowego statusu
    [SerializeField] float afflictionChance;
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
        player.events.OnAfflictionAppliedEvent.RemoveListener(BlessingLogicOnAfflictionApplied);
    }
    public void BlessingLogicOnAfflictionApplied()
    {
        EnemyData enemy = player.events.AfflictionAppliedEventData.target.GetComponent<EnemyData>();
        int rand = Random.Range(0, (int)ENUM_DamageType.Piercing);
        enemy.afflictions.ApplyAfflicion((ENUM_DamageType)rand);
    }
}
