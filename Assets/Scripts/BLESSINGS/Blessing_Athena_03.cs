using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Blessing_Athena_03 : Blessing_Base
{
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;
    float currentDamage;
    int rand;

    public override void Apply()
    {
        player = GetComponentInParent<PlayerData>();
        player.events.OnEnemyWeaponHitEvent.AddListener(BlessingLogic);

    }

    public override string GetDescription()
    {
        string text = "Damage of every shot varies between " + minDamage + "% and " + maxDamage + "%.";
        return text;
    }

    public override void Remove()
    {
        player.events.OnEnemyWeaponHitEvent.RemoveListener(BlessingLogic);
    }
    void BlessingLogic()
    {
        currentDamage = player.hitInfo.GetCurrentDamage();
        rand = Random.Range(minDamage, maxDamage);
        currentDamage *= (float)rand / 100;
        player.hitInfo.SetCurrentDamage(currentDamage);
    }
}
