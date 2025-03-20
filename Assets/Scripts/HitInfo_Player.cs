using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo_Player : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] EnemyData enemy;
    [SerializeField] bool isWeakSpot;

    public void Calculate()
    {
        float damage = gun.baseStats.basedamage.damage;
        if(isWeakSpot == true)
        {
            damage *= 2;
        }

        damage = CalculateCrit(damage);
        CheckIfAffliction();

    }

    float CalculateCrit(float damageCurrent)
    {
        float damage = damageCurrent;
        float critChance = gun.baseStats.critChance;
        int random;
        bool isCrit = false;


        while(critChance > 0)
        {
            random = Random.Range(0, 100);
            if(random <= critChance)
            {
                damage *= gun.baseStats.critMultiplier;
                isCrit = true;
                gun.playerData.events.OnCriticalHit.Invoke();
            }
            critChance -= 100;
        }

        if(isCrit == false)
        {
            gun.playerData.events.OnNonCriticalHit.Invoke();
        }

        return damage;
    }
    void CheckIfAffliction()
    {
        int random = Random.Range(0,100);

        if(random <= gun.baseStats.afflictionChance)
        {
            gun.playerData.events.OnAfflictionApplied.Invoke();
            enemy.afflictions.ApplyAfflicion(gun.baseStats.basedamage.damageType);
        }
    }

    public void SetData(Gun newGun, EnemyData newEnemy, bool weak)
    {
        gun = newGun;
        enemy = newEnemy;
        isWeakSpot = weak;
    }
}
