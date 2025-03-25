using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("REF")]
    public PlayerUI ui;
    public PlayerData data;

    [Header("Data")]
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
        if(data.finalStats.stamina < val)
        {
            regenerateStamina = true;
            return false;
        }

        regenerateStamina = true;
        //ReduceStamina(val);
        return true;
    }
    public void ReduceStamina(float val)
    {
        data.finalStats.stamina -= val;
        regenerateStamina = true;
        staminaRegenTimer = data.finalStats.staminaDelay;
        ui.UpdateStaminaBar(data.finalStats.stamina / data.finalStats.staminaMax);
    }
    public void DealDamage(float val)
    {
        if(data.finalStats.shield > 0)
        {
            // Damage is more than current Shield
            if(val > data.finalStats.shield)
            {
                val -= data.finalStats.shield;
                data.finalStats.shield = 0;
                data.events.OnShieldDepleted.Invoke();
            }
            // Damage is less than current shield
            else
            {
                float temp = data.finalStats.shield;
                data.finalStats.shield -= val;
                val -= temp;
            }
            regenerateShield = true;
            shieldRegenTimer = data.finalStats.shieldDelay;
            ui.UpdateShieldBar(data.finalStats.shield/ data.finalStats.shieldMax);
        }

        if(val > 0)
        {
            data.finalStats.health -= val;
            ui.UpdateHealthBar(data.finalStats.health / data.finalStats.healthMax);


            if (data.finalStats.health <= 0)
            {
                Debug.LogError("PLAYER DIES");
            }
        }
    }
    public void AddCurrentHealth(float val)
    {
        data.finalStats.health += val;
        if(data.finalStats.health >= data.finalStats.healthMax)
        {
            data.finalStats.health = data.finalStats.healthMax;
        }
        ui.UpdateHealthBar(data.finalStats.health / data.finalStats.healthMax);
    }
    public void DecreaseHealth(float val)
    {
        data.finalStats.health += val;
        if (data.finalStats.health <= 0)
        {
            Debug.LogError("PLAYER DIES");
        }
        ui.UpdateHealthBar(data.finalStats.health / data.finalStats.healthMax);
    }
    public void DecreaseShield(float val)
    {
        data.finalStats.shield -= val;
        if(data.finalStats.shield < 0)
        {
            data.finalStats.shield = 0;
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
            data.finalStats.stamina += data.finalStats.staminaRegen * Time.deltaTime;
            if(data.finalStats.stamina > data.finalStats.staminaMax)
            {
                data.finalStats.stamina = data.finalStats.staminaMax;
                regenerateStamina = false;
            }
            ui.UpdateStaminaBar(data.finalStats.stamina / data.finalStats.staminaMax);
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
            data.events.OnShieldRegenerationStart.Invoke();
            shieldRegenTimer = 0;
            data.finalStats.shield += data.finalStats.shieldRegen * Time.deltaTime;
            if (data.finalStats.shield > data.finalStats.shieldMax)
            {
                data.finalStats.shield = data.finalStats.shieldMax;
                regenerateShield = false;
            }
            ui.UpdateShieldBar(data.finalStats.shield / data.finalStats.shieldMax);
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
            data.finalStats.health += data.finalStats.healthRegen * Time.deltaTime;
            if (data.finalStats.health > data.finalStats.healthMax)
            {
                data.finalStats.health = data.finalStats.healthMax;
                regenerateHealth = false;
            }
            ui.UpdateHealthBar(data.finalStats.health / data.finalStats.healthRegen);
        }
    }
}
