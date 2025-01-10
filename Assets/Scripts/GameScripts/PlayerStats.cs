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

    float staminaRegenTimer;
    bool regenerateStamina;
    public float accuracy;

    public bool CheckIfCanUseStamina(float val)
    {
        // Jest mniej staminy niz wymaga akcja
        if(finalStats.stamina < val)
        {
            regenerateStamina = true;
            return false;
        }

        //ReduceStamina(val);
        return true;
    }
    public void ReduceStamina(float val)
    {
        finalStats.stamina -= val;
        regenerateStamina = true;
        staminaRegenTimer = finalStats.staminaDelay;
        ui.UpdateStaminaBar(finalStats.stamina / finalStats.staminaMax);
    }
    public void ModifyHealth(float val)
    {
        finalStats.health += val;
        ui.UpdateHealthBar(finalStats.health / finalStats.healthMax);


        if(finalStats.health <= 0)
        {
            Debug.LogError("PLAYER DIES");
        }
    }

    private void Update()
    {
        StaminaRegeneration();
    }

    void StaminaRegeneration()
    {
        if(regenerateStamina == false)
        {
            return;
        }

        staminaRegenTimer -= Time.deltaTime;
        if(staminaRegenTimer <= 0)
        {
            staminaRegenTimer = 0;
            finalStats.stamina += finalStats.staminaRegen * Time.deltaTime;
            if(finalStats.stamina > finalStats.staminaMax)
            {
                finalStats.stamina = finalStats.staminaMax;
                regenerateStamina = false;
            }
            ui.UpdateStaminaBar(finalStats.stamina / finalStats.staminaMax);
        }
    }
    void HealthRegeneration()
    {

    }

    public void CreateFinalStats()
    {
        finalStats.CombineStats(baseStats, bonusStats);
        finalStats.stamina = finalStats.staminaMax;
        finalStats.health = finalStats.healthMax;
        finalStats.shield = finalStats.shieldMax;
    }

}
