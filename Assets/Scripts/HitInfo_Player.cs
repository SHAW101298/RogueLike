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
    float percentOfHealthDealt;
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
        damage += gun.playerData.stats.globalDamageModifier * damage;
        damage += gun.playerData.stats.globalGunDamageModifier * damage;
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

    void CalculateCrit()
    {
        float critChance = gun.baseStats.critChance;
        critChance += gun.playerData.stats.globalCriticalChanceModifier;
        int random;
        bool isCrit = false;


        while(critChance > 0)
        {
            random = Random.Range(0, 100);
            if(random <= critChance)
            {
                damageInfo.SetDamage(damageInfo.GetDamageAmount() * (gun.baseStats.critMultiplier + gun.playerData.stats.globalCriticalMultiplierModifier));
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
        afflictionChance += gun.playerData.stats.globalAfflictionChanceModifier;

        while(afflictionChance > 0)
        {
            random = Random.Range(0, 100);
            if (random <= gun.baseStats.afflictionChance)
            {
                gun.playerData.events.OnAfflictionAppliedEvent.Invoke();
                enemy.afflictions.ApplyAfflicion(gun.baseStats.basedamage.damageType);
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
        switch(damageType)
        {
            case ENUM_DamageType.Heat:
                damage += damage * gun.playerData.stats.globalElementalDamageModifier;
                damage += damage * gun.playerData.stats.globalHeatDamageModifier;
                break;
            case ENUM_DamageType.Ice:
                damage += damage * gun.playerData.stats.globalElementalDamageModifier;
                damage += damage * gun.playerData.stats.globalIceDamageModifier;
                break;
            case ENUM_DamageType.Toxin:
                damage += damage * gun.playerData.stats.globalElementalDamageModifier;
                damage += damage * gun.playerData.stats.globalToxinDamageModifier;
                break;
            case ENUM_DamageType.Electricity:
                damage += damage * gun.playerData.stats.globalElementalDamageModifier;
                damage += damage * gun.playerData.stats.globalElectricityDamageModifier;
                break;
            case ENUM_DamageType.Chaos:
                damage += damage * gun.playerData.stats.globalElementalDamageModifier;
                damage += damage * gun.playerData.stats.globalChaosDamageModifier;
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
            Calculate();
        }
    }


}
