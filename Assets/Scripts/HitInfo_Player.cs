using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_HitResult
{
    firstHit,
    kill,
    halfHP,
    alive
}
public class HitResult
{
    ENUM_HitResult result;
    public float percentOfHealthDealt;
    public bool firstHit;
    public bool halfhealth;
    public bool death;

    public ENUM_HitResult GetResult()
    {
        return result;
    }
    public float GetHealthPercent()
    {
        return percentOfHealthDealt;
    }

    public void SetData(ENUM_HitResult newResult, float newHealth)
    {
        result = newResult;
        percentOfHealthDealt = newHealth;
    }

    public void TriggerEvents()
    {
        PlayerData player = PlayerList.Instance.GetMyPlayer();
        if(firstHit == true)
        {
            player.events.OnFirstHit.Invoke();
        }
        if(halfhealth == true)
        {
            player.events.OnHalfHealthTap.Invoke();
        }
        if(death == true)
        {
            player.events.OnEnemyKillEvent.Invoke();
        }
    }
}
[System.Serializable]
public class DamageInfo
{
    float damage;
    ENUM_DamageType type;
    

    public void SetData(float newDamage, ENUM_DamageType newType)
    {
        damage = newDamage;
        type = newType;
    }
    public void SetData(DamageData data)
    {
        damage = data.damage;
        type = data.damageType;
    }
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
    public float GetDamageAmount()
    {
        return damage;
    }
    public ENUM_DamageType GetDamageType()
    {
        return type;
    }
    

}

public class HitInfo_Player : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] EnemyData enemy;
    [SerializeField] bool isWeakSpot;

    [SerializeField] bool Simulate;
    [SerializeField] public DamageInfo damageInfo;

    private void Start()
    {
        damageInfo = new DamageInfo();
    }

    public void CalculateForAbility()
    {

    }
    public void CalculateForGun()
    {
        damageInfo.SetData(gun.baseStats.basedamage.damage, gun.baseStats.basedamage.damageType);
        float damage = damageInfo.GetDamageAmount();
        damage += gun.playerData.finalStats.damageModifier * damage;
        damage += gun.playerData.finalStats.gunDamageModifier * damage;
        damageInfo.SetDamage(damage);

        gun.playerData.events.OnEnemyWeaponHitEvent.Invoke();

        if (isWeakSpot == true)
        {
            damageInfo.SetDamage(damageInfo.GetDamageAmount() * 2);
        }
        IncreaseByElementalType();
        CalculateCrit();
        ReduceByResistance();
        IncreaseByAfflictionModdifiers();
        CheckIfAffliction();
    }

    /*
    public void Calculate()
    {
        damageInfo.SetData(gun.baseStats.basedamage.damage, gun.baseStats.basedamage.damageType);
        if(isWeakSpot == true)
        {
            damageInfo.SetDamage(damageInfo.GetDamageAmount()*2);
        }
        IncreaseByElementalType();
        CalculateCrit();
        ReduceByResistance();
        IncreaseByAfflictionModdifiers();
        CheckIfAffliction();
    }
    */
    void CalculateCrit()
    {
        float critChance = gun.baseStats.critChance;
        critChance += gun.playerData.finalStats.criticalChanceModifier;
        int random;
        bool isCrit = false;


        while(critChance > 0)
        {
            random = Random.Range(0, 100);
            if(random <= critChance)
            {
                damageInfo.SetDamage(damageInfo.GetDamageAmount() * (gun.modifiedStats.critMultiplier + gun.playerData.finalStats.criticalDamageModifier));
                isCrit = true;
                gun.playerData.events.OnCriticalHitEvent.Invoke();
            }
            critChance -= 100;
        }

        if(isCrit == false)
        {
            gun.playerData.events.OnNonCriticalHitEvent.Invoke();
        }
    }
    void CheckIfAffliction()
    {
        //Debug.Log("Check if Affliction");
        int random = Random.Range(0,100);
        float afflictionChance = gun.baseStats.afflictionChance;
        afflictionChance += gun.playerData.finalStats.afflictionChanceModifier;

        while(afflictionChance > 0)
        {
            random = Random.Range(0, 100);
            if (random <= afflictionChance)
            {
                enemy.afflictions.ApplyAfflicion(gun.baseStats.basedamage.damageType);
                gun.playerData.events.AfflictionAppliedEventData.SetData(enemy.afflictions.GetAffliction(gun.modifiedStats.basedamage.damageType), enemy);
                gun.playerData.events.OnAfflictionAppliedEvent.Invoke();
                afflictionChance -= 100;
            }
        }
        
    }
    void ReduceByResistance()
    {
        //Debug.Log("Reduce By Resistance");
        float res = enemy.stats.GetFlatResist(gun.baseStats.basedamage.damageType);
        damageInfo.SetDamage(damageInfo.GetDamageAmount() - res);

        res = enemy.stats.GetPercentResist(gun.baseStats.basedamage.damageType);
        res /= 100;
        res = damageInfo.GetDamageAmount() * res;
        damageInfo.SetDamage(damageInfo.GetDamageAmount() - res);
    }
    void IncreaseByAfflictionModdifiers()
    {
        //Debug.Log("Increase By Affliction Modifiers");
        float modifier;
        int typeLimit = (int)ENUM_DamageType.Piercing;

        for(int i = 0; i < typeLimit; i++)
        {
            // Is enemy Afflicted ?
            if(enemy.afflictions.ReturnAfflictionState( (ENUM_DamageType)i ) == true)
            {
                modifier = gun.baseStats.damageMultipliersOnAffliction.GetData(i) / 100;
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
        damage += damage * gun.playerData.finalStats.totalElementalDamageModifier;
        switch (damageType)
        {
            case ENUM_DamageType.Heat:
                damage += damage * gun.playerData.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Heat);
                break;
            case ENUM_DamageType.Ice:
                damage += damage * gun.playerData.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Ice);
                break;
            case ENUM_DamageType.Toxin:
                damage += damage * gun.playerData.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Toxin);
                break;
            case ENUM_DamageType.Electricity:
                damage += damage * gun.playerData.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Electricity);
                break;
            case ENUM_DamageType.Chaos:
                damage += damage * gun.playerData.finalStats.elementalDamageModifier.GetData(ENUM_DamageType.Chaos);
                break;
            case ENUM_DamageType.Piercing:
                break;
            default:
                Debug.LogError("Unknown Damage TYPE");
                break;
        }
        damageInfo.SetDamage(damage);
    }


    public void SetData(Gun newGun, EnemyData newEnemy, bool weak)
    {
        gun = newGun;
        enemy = newEnemy;
        isWeakSpot = weak;
    }

    public void SetCurrentDamage(float damage)
    {
        damageInfo.SetDamage(damage);
    }
    public float GetCurrentDamage()
    {
        return damageInfo.GetDamageAmount();
    }

    private void Update()
    {
        if(Simulate == true)
        {
            Simulate = false;
            CalculateForGun();
        }
    }


}
