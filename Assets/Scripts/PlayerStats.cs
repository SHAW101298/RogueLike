using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("REF")]
    public PlayerUI ui;

    [Header("Data")]

    public Stats baseStats;
    public Stats bonusStats;
    public Stats finalStats;


    public int health;
    public float staminaCurrent;
    public float staminaMax;
    public float staminaRegenAmount;

    public float staminaRegenDelay;
    float staminaRegenTimer;
    bool regenerateStamina;
    public float accuracy;

    public bool CheckIfCanUseStamina(float val)
    {
        // Jest mniej staminy niz wymaga akcja
        if(staminaCurrent < val)
        {
            regenerateStamina = true;
            return false;
        }

        staminaCurrent -= val;
        regenerateStamina = true;
        staminaRegenTimer = staminaRegenDelay;
        ui.UpdateStaminaBar(staminaCurrent / staminaMax);
        return true;
    }
    public void ReduceStamina(float val)
    {
        staminaCurrent -= val;
        regenerateStamina = true;
        staminaRegenTimer = staminaRegenDelay;
        ui.UpdateStaminaBar(staminaCurrent / staminaMax);
    }

    private void Update()
    {
        StaminaRegeneration();
        
    }

    void StaminaRegeneration()
    {
        if (regenerateStamina == false)
            return;

        staminaRegenTimer -= Time.deltaTime;
        if (staminaRegenTimer <= 0)
        {
            staminaRegenTimer = 0;
            staminaCurrent += staminaRegenAmount * Time.deltaTime;
            if(staminaCurrent > staminaMax)
            {
                staminaCurrent = staminaMax;
                regenerateStamina = false;
            }
            ui.UpdateStaminaBar(staminaCurrent/staminaMax);
        }
    }

    void CreateFinalStats()
    {
        float missingHealth = baseStats.health;
        if(finalStats != null)
        {
             missingHealth = finalStats.healthMax - finalStats.health;
        }
        finalStats = new Stats(baseStats, bonusStats);
        finalStats.health = finalStats.healthMax - missingHealth;
    }

}
