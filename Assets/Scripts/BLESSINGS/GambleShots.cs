using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleShots : Blessing_Base
{
    [SerializeField] float min;
    [SerializeField] float max;
    float damage;
    float rand;
    public override void Apply()
    {
        player = GetComponentInParent<PlayerData>();
        player.events.OnEnemyWeaponHitEvent.AddListener(BlessingLogic);
    }
    public override void Remove()
    {
        player.events.OnEnemyWeaponHitEvent.RemoveListener(ReactOnHit);
    }

    public override string GetDescription()
    {
        string text = "Damage of each shot will vary from " + min + "% to " + max + "%.";
        return text;
    }

    public void BlessingLogic()
    {
        damage = player.hitInfo.GetCurrentDamage();
        rand = Random.Range(min, max);
        damage *= rand;
        player.hitInfo.SetCurrentDamage(damage);
    }
}
