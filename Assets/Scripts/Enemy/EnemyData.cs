using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : UnitData
{
    public CharacterController controller;
    public Stats stats;
    public Afflictions afflictions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HitEnemy(GunDamageData damageData)
    {
        

    }
    public void HitEnemy(List<GunDamageData> dealtDamage)
    {
        foreach(GunDamageData damage in dealtDamage)
        {
            
        }
    }
    public float HitEnemy(GunStats gunStats)
    {
        float dealtDamage = 0;

        float damage;
        float resistanceFlat;
        float resistancePercentage;
        for(int i = 0; i < gunStats.damageArray.Count; i++)
        {
            damage = gunStats.damageArray[i].damage;
            resistanceFlat = stats.GetFlatResist( gunStats.damageArray[i].damageType );
            resistancePercentage = stats.GetPercentResist(gunStats.damageArray[i].damageType);

            // Check for multipliers when afflicted
            for(int j = 0; j < gunStats.damageMultipliersOnAffliction.multipliers.Length; j++)
            {
                // Found multiplier
                if (gunStats.damageMultipliersOnAffliction.multipliers[j] != 1 )
                {
                    // Check if under affliction
                    bool afflictionCheck = afflictions.ReturnAfflictionState((ENUM_DamageType)j);
                    if(afflictionCheck == true)
                    {
                        damage *= gunStats.damageMultipliersOnAffliction.multipliers[j];
                    }
                }
            }

            damage -= resistanceFlat;
            float reduction = (damage / 100) * resistancePercentage;
            damage -= reduction;
            dealtDamage += damage;
        }
        Debug.Log(dealtDamage);
        return dealtDamage;
    }

}
