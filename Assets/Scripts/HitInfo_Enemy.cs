using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo_Enemy : MonoBehaviour
{
    [SerializeField] public DamageInfo damageInfo;
    [SerializeField] PlayerData player;
    [SerializeField] EnemyData enemy;
    [SerializeField] Enemy_Weapon weapon;

    private void Start()
    {
        player = GetComponent<PlayerData>();
    }

    public void CalculateForProjectile()
    {
        weapon = enemy.GetComponent<Enemy_Weapon>();
        damageInfo.SetData(weapon.damage);
        float damage = damageInfo.GetDamageAmount();
        damage += enemy.finalStats.damageModifier * damage;
        damage += enemy.finalStats.gunDamageModifier * damage;
        damageInfo.SetDamage(damage);

        IncreaseByElementalType();
        CalculateCrit();
        ReduceByResistance();
        IncreaseByAfflictionModdifiers();
        CheckIfAffliction();
    }
    public void SetData(EnemyData owner, PlayerData hitPlayer)
    {
        enemy = owner;
        player = hitPlayer;
    }
    public void CalculateForAbility()
    {

    }


    void CalculateCrit()
    {
        float critChance = weapon.critChance;
        critChance += enemy.finalStats.criticalChanceModifier;
        int random;
        bool isCrit = false;


        while (critChance > 0)
        {
            random = Random.Range(0, 100);
            if (random <= critChance)
            {
                damageInfo.SetDamage(damageInfo.GetDamageAmount() * (weapon.critMultiplier + enemy.finalStats.criticalDamageModifier));
                isCrit = true;
                player.events.OnTakeCriticalHit.Invoke();
            }
            critChance -= 100;
        }

        if (isCrit == false)
        {
            player.events.OnTakeNonCritHit.Invoke();
        }
    }
    void CheckIfAffliction()
    {
        //Debug.Log("Check if Affliction");
        int random = Random.Range(0, 100);
        float afflictionChance = weapon.afflictionChance;
        afflictionChance += enemy.finalStats.afflictionChanceModifier;

        while (afflictionChance > 0)
        {
            random = Random.Range(0, 100);
            if (random <= afflictionChance)
            {
                player.afflictions.ApplyAfflicion(weapon.damage.damageType);
                player.events.AfflictionAppliedEventData.SetData(player.afflictions.GetAffliction(weapon.damage.damageType), enemy);
                player.events.OnAfflictionReceived.Invoke();
                afflictionChance -= 100;
            }
        }

    }
    void ReduceByResistance()
    {
        //Debug.Log("Reduce By Resistance");
        float res = player.finalStats.GetFlatResist(weapon.damage.damageType);
        damageInfo.SetDamage(damageInfo.GetDamageAmount() - res);

        res = player.finalStats.GetPercentResist(weapon.damage.damageType);
        res /= 100;
        res = damageInfo.GetDamageAmount() * res;
        damageInfo.SetDamage(damageInfo.GetDamageAmount() - res);
    }
    void IncreaseByAfflictionModdifiers()
    {
        //Debug.Log("Increase By Affliction Modifiers");
        float modifier;
        int typeLimit = (int)ENUM_DamageType.Piercing;

        for (int i = 0; i < typeLimit; i++)
        {
            // Is enemy Afflicted ?
            if (player.afflictions.ReturnAfflictionState((ENUM_DamageType)i) == true)
            {
                modifier = weapon.afflictionModifiers.GetData(i) / 100;
                if (modifier != 0)
                {
                    modifier = damageInfo.GetDamageAmount() * modifier;
                    damageInfo.SetDamage(damageInfo.GetDamageAmount() + modifier);
                }
            }
        }
    }
    void IncreaseByElementalType()
    {
        ENUM_DamageType damageType = damageInfo.GetDamageType();
        float damage = damageInfo.GetDamageAmount();
        damage += damage * enemy.finalStats.totalElementalDamageModifier;
        switch (damageType)
        {
            case ENUM_DamageType.Heat:
                damage += damage * enemy.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Heat);
                break;
            case ENUM_DamageType.Ice:
                damage += damage * enemy.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Ice);
                break;
            case ENUM_DamageType.Toxin:
                damage += damage * enemy.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Toxin);
                break;
            case ENUM_DamageType.Electricity:
                damage += damage * enemy.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Electricity);
                break;
            case ENUM_DamageType.Chaos:
                damage += damage * enemy.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Chaos);
                break;
            case ENUM_DamageType.Piercing:
                break;
            default:
                Debug.LogError("Unknown Damage TYPE");
                break;
        }
        damageInfo.SetDamage(damage);
    }
}
