using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo_Player : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] EnemyData enemy;
    [SerializeField] bool isWeakSpot;

    [SerializeField] bool Simulate;
    [SerializeField] float currentDamage;

    public void Calculate()
    {
        //Debug.Log("Calculate");
        currentDamage = gun.baseStats.basedamage.damage;
        //float damage = gun.baseStats.basedamage.damage;
        if(isWeakSpot == true)
        {
            currentDamage *= 2;
        }
        //Debug.Log("Current Damage = " + currentDamage);
        CalculateCrit();
        //Debug.Log("Current Damage = " + currentDamage);
        ReduceByResistance();
        //Debug.Log("Current Damage = " + currentDamage);
        IncreaseByAfflictionModdifiers();
        //Debug.Log("Current Damage = " + currentDamage);
        CheckIfAffliction();
        //Debug.Log("Current Damage = " + currentDamage);
    }

    void CalculateCrit()
    {
        Debug.Log("Calculate Crit");
        float critChance = gun.baseStats.critChance;
        int random;
        bool isCrit = false;


        while(critChance > 0)
        {
            random = Random.Range(0, 100);
            if(random <= critChance)
            {
                currentDamage *= gun.baseStats.critMultiplier;
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
        Debug.Log("Check if Affliction");
        int random = Random.Range(0,100);

        if(random <= gun.baseStats.afflictionChance)
        {
            gun.playerData.events.OnAfflictionAppliedEvent.Invoke();
            enemy.afflictions.ApplyAfflicion(gun.baseStats.basedamage.damageType);
        }
    }
    void ReduceByResistance()
    {
        //Debug.Log("Reduce By Resistance");
        
        float res = enemy.stats.GetFlatResist(gun.baseStats.basedamage.damageType);
        currentDamage -= res;

        res = enemy.stats.GetPercentResist(gun.baseStats.basedamage.damageType);
        res /= 100;
        currentDamage = currentDamage - (currentDamage * res);
    }
    void IncreaseByAfflictionModdifiers()
    {
        Debug.Log("Increase By Affliction Modifiers");
        float modifier;

        for(int i = 0; i < (int)ENUM_DamageType.Piercing;i++)
        {
            // Is enemy Afflicted ?
            if(enemy.afflictions.ReturnAfflictionState( (ENUM_DamageType)i ) == true)
            {
                modifier = gun.baseStats.damageMultipliersOnAffliction.GetData(i) / 100;
                if (modifier != 0)
                {
                    currentDamage = currentDamage + (currentDamage * modifier);
                }
            }
            
        }
    }


    public void SetData(Gun newGun, EnemyData newEnemy, bool weak)
    {
        gun = newGun;
        enemy = newEnemy;
        isWeakSpot = weak;
    }
    public float GetCurrentDamage()
    {
        return currentDamage;
    }
    public void SetCurrentDamage(float damage)
    {
        currentDamage = damage;
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
