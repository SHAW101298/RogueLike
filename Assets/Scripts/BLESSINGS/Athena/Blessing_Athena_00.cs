using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_00 : Blessing_Base
{
    [SerializeField] float damageIncrease;
    float currentDamage;
    float currentModifier;
    public override void Apply()
    {
        GetParent();
        player.events.OnEnemyWeaponHitEvent.AddListener(BlessingLogicWeapon);
        player.events.OnEnemyAbilityHitEvent.AddListener(BlessingLogicAbility);
    }

    public override string GetDescription()
    {
        string text = "Increase damage by " + damageIncrease*10 + "% for every 10% of shield.";
        return text;
    }

    public override void Remove()
    {
        player.events.OnEnemyWeaponHitEvent.RemoveListener(BlessingLogicWeapon);
        player.events.OnEnemyAbilityHitEvent.RemoveListener(BlessingLogicAbility);
    }

    void BlessingLogicWeapon()
    {
        Debug.Log("Activating " + title);
        currentDamage = player.events.hitInfoPlayer.GetCurrentDamage();
        currentModifier = player.finalStats.shield / player.finalStats.shieldMax;
        currentModifier *= damageIncrease;
        currentDamage = currentDamage + (currentDamage * currentModifier);
        player.events.hitInfoPlayer.SetCurrentDamage(currentDamage);
    }
    void BlessingLogicAbility()
    {
        Debug.Log("Activating " + title);
        currentDamage = player.events.hitInfoPlayer.GetCurrentDamage();
        currentModifier = player.finalStats.shield / player.finalStats.shieldMax;
        currentModifier *= damageIncrease;
        currentDamage = currentDamage + (currentDamage * currentModifier);
        player.events.hitInfoPlayer.SetCurrentDamage(currentDamage);
    }

}
