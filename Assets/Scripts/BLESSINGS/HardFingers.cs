using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardFingers : Blessing_Base
{
    [SerializeField] float damageIncrease;
    [SerializeField] PlayerData player;

    public override void Apply()
    {
        player = GetComponentInParent<PlayerData>();
        player.events.OnNonCriticalHitEvent.AddListener(ReactToNonCrit);
    }

    public override string GetDescription()
    {
        string text = "Increase Non-Crit hits damage by " + damageIncrease.ToString() + "%.";
        return text;
    }

    public void ReactToNonCrit()
    {
        float damage = player.hitInfo.GetCurrentDamage();
        damage = damage + (damage * (damageIncrease / 100));
        player.hitInfo.SetCurrentDamage(damage);
    }
}
