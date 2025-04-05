using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_12 : Blessing_Base
{
    // Po trafieniu krytycznym, nastêpny strza³ ma zwiêkszone obra¿enia
    [SerializeField] float damageIncreasePercent;
    bool blessingActive;
    public override void Apply()
    {
        GetParent();
        player.events.OnCriticalHitEvent.AddListener(BlessingLogicOnCriticalHit);
        player.events.OnEnemyWeaponHitEvent.AddListener(BlessingLogicOnEnemyWeaponHitEvent);
    }

    public override string GetDescription()
    {
        string text = "After a critical hit, increase damage of next shot by " + damageIncreasePercent + "%.";
        return text;
    }

    public override void Remove()
    {
        player.events.OnCriticalHitEvent.RemoveListener(BlessingLogicOnCriticalHit);
        player.events.OnEnemyWeaponHitEvent.RemoveListener(BlessingLogicOnEnemyWeaponHitEvent);
    }
    public void BlessingLogicOnCriticalHit()
    {
        blessingActive = true;
    }
    public void BlessingLogicOnEnemyWeaponHitEvent()
    {
        if(blessingActive == true)
        {
            float curentDamage = player.hitInfo.GetCurrentDamage();
            curentDamage *= (damageIncreasePercent / 100);
            player.hitInfo.SetCurrentDamage(curentDamage);
            blessingActive = false;
        }
    }
}