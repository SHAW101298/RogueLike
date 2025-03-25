using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_07 : Blessing_Base
{
    [SerializeField] float damageIncrease;
    float currentDamage;
    float currentModifier;
    public override void Apply()
    {
        GetParent();
        player.events.OnEnemyWeaponHitEvent.AddListener(BlessingLogicOnHit);
        player.events.OnEnemyAbilityHitEvent.AddListener(BlessingLogicOnHit);
    }

    public override string GetDescription()
    {
        string text = "Increase damage by " + damageIncrease*10 + "% for every 10% of health missing.";
        return text;
    }

    public override void Remove()
    {
        throw new System.NotImplementedException();
    }
    public void BlessingLogicOnHit()
    {
        currentDamage = player.events.hitInfoPlayer.GetCurrentDamage();
        currentModifier = player.finalStats.health / player.finalStats.healthMax;
        currentModifier *= damageIncrease;
        currentDamage *= currentModifier;
        player.events.hitInfoPlayer.SetCurrentDamage(currentDamage);
    }
}
