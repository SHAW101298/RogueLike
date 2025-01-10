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
    float shieldRegenTimer;
    bool regenerateShield;
    float healthRegenTimer;
    bool regenerateHealth;

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
    public void DecreaseHealth(float val)
    {
        if(finalStats.shield > 0)
        {
            if(val > finalStats.shield)
            {
                val -= finalStats.shield;
                finalStats.shield = 0;
            }
            else
            {
                float temp = finalStats.shield;
                finalStats.shield -= val;
                val -= temp;
            }
            regenerateShield = true;
            shieldRegenTimer = finalStats.shieldDelay;
            ui.UpdateShieldBar(finalStats.shield/finalStats.shieldMax);
        }

        if(val > 0)
        {
            finalStats.health -= val;
            ui.UpdateHealthBar(finalStats.health / finalStats.healthMax);


            if (finalStats.health <= 0)
            {
                Debug.LogError("PLAYER DIES");
            }
        }
    }
    public void DecreaseShield(float val)
    {
        finalStats.shield -= val;
        if(finalStats.shield < 0)
        {
            finalStats.shield = 0;
        }
    }

    private void Update()
    {
        StaminaRegeneration();
        ShieldRegeneration();
        HealthRegeneration();
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
    void ShieldRegeneration()
    {
        if (regenerateShield == false)
        {
            return;
        }

        shieldRegenTimer -= Time.deltaTime;
        if (shieldRegenTimer <= 0)
        {
            shieldRegenTimer = 0;
            finalStats.shield += finalStats.shieldRegen * Time.deltaTime;
            if (finalStats.shield > finalStats.shieldMax)
            {
                finalStats.shield = finalStats.shieldMax;
                regenerateStamina = false;
            }
            ui.UpdateShieldBar(finalStats.shield / finalStats.shieldMax);
        }
    }
    void HealthRegeneration()
    {
        if (regenerateHealth == false)
        {
            return;
        }

        healthRegenTimer -= Time.deltaTime;
        if (healthRegenTimer <= 0)
        {
            healthRegenTimer = 0;
            finalStats.health += finalStats.healthRegen * Time.deltaTime;
            if (finalStats.health > finalStats.healthMax)
            {
                finalStats.health = finalStats.healthMax;
                regenerateStamina = false;
            }
            ui.UpdateHealthBar(finalStats.health / finalStats.healthRegen);
        }
    }

    public void CreateFinalStats()
    {
        finalStats.CombineStats(baseStats, bonusStats);
        finalStats.stamina = finalStats.staminaMax;
        finalStats.health = finalStats.healthMax;
        finalStats.shield = finalStats.shieldMax;
    }

}
