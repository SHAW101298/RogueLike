using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
using UnityEngine;

public class EnemyData : UnitData
{
    public CharacterController controller;
    public Stats stats;
    public Afflictions afflictions;
    public Enemy_AI ai;
    public SpriteRenderer healthBar;

    [SerializeField] float baseGoldReward;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer <= 0.25f)
        {
            healthBar.transform.LookAt(PlayerList.Instance.GetMyPlayer().gameObject.transform.position);
            timer = 0;
        }
    }
    public float HitEnemy(BulletInfo info)
    {
        if(ai.CheckIfIdle() == true)
        {
            ai.chase.chaseDistance *= 2;
            
        }

        float fullDamage = 0;
        float calcDamage = 0;
        float dmgModifier = 0;
        float flatRes = 0;
        float percRes = 0;
        float reduction = 0;
        DamageData dmgData;

        for(int i = 0; i < info.damageData.Count; i++)
        {
            dmgData = info.damageData[i];
            // Base Damage
            calcDamage = dmgData.damage;
            // Damage modifier if target under affliction
            dmgModifier = info.damageModifierWhenAfflicted.GetData(dmgData.damageType);

            flatRes = stats.GetFlatResist(dmgData.damageType);
            percRes = stats.GetPercentResist(dmgData.damageType);

            calcDamage -= flatRes;
            reduction = (calcDamage / 100) * percRes;
            calcDamage -= reduction;

            if (dmgModifier != 1)
            {
                // Modify damage if afflicted
                if (afflictions.ReturnAfflictionState(dmgData.damageType) == true)
                {
                    calcDamage *= dmgModifier;
                }
            }

            // Check if scored a crit
            if(CheckIfCrit(info.critChance) == true )
            {
                calcDamage *= info.critDamageMultiplier;
            }
            // Check if we apply status
            if(CheckIfAffliction(info.afflictionChance) == true)
            {
                afflictions.ApplyAfflicion(dmgData.damageType);
            }

            fullDamage += calcDamage;
        }

        //Debug.Log("Full Damage = " + fullDamage);
        if(fullDamage < 0)
        {
            fullDamage = 1;
        }
        ModifyHealth(-fullDamage);
        Vector3 newScale = Vector3.one;
        newScale.x = stats.health / stats.healthMax;
        healthBar.transform.localScale = newScale;
        return fullDamage;
    }

    /*
    public float HitEnemy(Gun gun)
    {
        GunStats gunStats = gun.modifiedStats;
        float dealtDamage = 0;

        float damage;
        float resistanceFlat;
        float resistancePercentage;

        // DAMAGE CALCULATION
        // dmg = ( (DMG * AfMod * CritMultiplier) - ResFlat ) - ResPerc
        for(int i = 0; i < gunStats.damageArray.Count; i++)
        {
            damage = gunStats.damageArray[i].damage;
            resistanceFlat = stats.GetFlatResist( gunStats.damageArray[i].damageType );
            resistancePercentage = stats.GetPercentResist(gunStats.damageArray[i].damageType);

            // Check for multipliers when afflicted
            for(int j = 0; j < gunStats.damageMultipliersOnAffliction.Count(); j++)
            {
                // Found multiplier
                if (gunStats.damageMultipliersOnAffliction.GetData(j) != 1 )
                {
                    // Check if under affliction
                    bool afflictionCheck = afflictions.ReturnAfflictionState((ENUM_DamageType)j);
                    if(afflictionCheck == true)
                    {
                        damage *= gunStats.damageMultipliersOnAffliction.GetData(j) ;
                    }
                }
            }
            damage *= gunStats.totalDamageMultiplier;
            if(CheckIfCrit(gunStats.critChance) == true)
            {
                gun.EVENT_ScoredCrit();
                damage *= gunStats.critMultiplier;
            }


            damage -= resistanceFlat;
            float reduction = (damage / 100) * resistancePercentage;
            damage -= reduction;
            dealtDamage += damage;
        }
        Debug.Log(dealtDamage);

        ModifyHealth(-dealtDamage);
        CheckIfAlive(gun);
        return dealtDamage;
    }
    */
    [ServerRpc]
    public void ModifyHealth_ServerRPC(float value)
    {
        //Debug.Log("Modyfing Health of Enemy");
        stats.health += value;
        CheckIfAlive();
    }

    void ModifyHealth(float value)
    {
        ModifyHealth_ServerRPC(value);
    }
    void CheckIfAlive()
    {
        if(stats.health <= 0)
        {
            Debug.Log("Begin Dying Procedure");
            ai.NetworkObject.Despawn(true);
            //Destroy(gameObject);
        }
    }

    bool CheckIfCrit(float critChance)
    {
        int rnd = Random.Range(0, 100);
        //Debug.Log("Rnd = " + rnd + " | chance = " + critChance);
        if (rnd < critChance)
        {
            return true;
        }
        return false;
    }
    bool CheckIfAffliction(float affChance)
    {
        int rnd = Random.Range(0, 100);
        //Debug.Log("Rnd = " + rnd + " | chance = " + affChance);
        if (rnd < affChance)
        {
            return true;
        }
        return false;
    }
    public void ActivateEnemy()
    {
        Debug.LogWarning("Implement AI activation");
        ai.ActivateAI();
    }
}
