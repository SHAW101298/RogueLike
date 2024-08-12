using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public float healthMax;
    public float health;
    public float healthRegen;
    public float healthDelay;

    public float staminaMax;
    public float stamina;
    public float staminaRegen;
    public float staminaDelay;

    public float shieldMax;
    public float shield;
    public float shieldRegen;
    public float shieldDelay;

    public float physicalFlatResistance;
    public float coldFlatResistance;
    public float heatFlatResistance;
    public float toxinFlatResistance;
    public float electricityFlatResistance;
    public float chaosFlatResistance;

    public float physicalPercentResistance;
    public float coldPercentResistance;
    public float heatPercentResistance;
    public float toxinPercentResistance;
    public float electricityPercentResistance;
    public float chaosPercentResistance;


    public Stats(Stats stats1, Stats stats2)
    {
        healthMax = stats1.healthMax + stats2.healthMax;
        healthRegen = stats1.healthRegen + stats2.healthRegen;
        healthDelay = stats1.healthDelay + stats2.healthDelay;
        staminaMax = stats1.staminaMax + stats2.staminaMax;
        staminaRegen = stats1.staminaRegen + stats2.staminaRegen;
        staminaDelay = stats1.staminaDelay + stats2.staminaDelay;
        shieldMax = stats1.shieldMax + stats2.shieldMax;
        shieldRegen = stats1.shieldRegen + stats2.shieldRegen;
        shieldDelay = stats1.shieldDelay + stats2.shieldDelay;
        physicalFlatResistance = stats1.physicalFlatResistance + stats2.physicalFlatResistance;
        coldFlatResistance = stats1.coldFlatResistance + stats2.coldFlatResistance;
        heatFlatResistance = stats1.heatFlatResistance + stats2.heatFlatResistance;
        toxinFlatResistance = stats1.toxinFlatResistance + stats2.toxinFlatResistance;
        electricityFlatResistance = stats1.electricityFlatResistance + stats2.electricityFlatResistance;
        chaosFlatResistance = stats1.chaosFlatResistance + stats2.chaosFlatResistance;
        physicalPercentResistance = stats1.physicalPercentResistance + stats2.physicalPercentResistance;
        coldPercentResistance = stats1.coldPercentResistance + stats2.coldPercentResistance;
        heatPercentResistance = stats1.heatPercentResistance + stats2.heatPercentResistance;
        toxinPercentResistance = stats1.toxinPercentResistance + stats2.toxinPercentResistance;
        electricityPercentResistance = stats1.electricityPercentResistance + stats2.electricityPercentResistance;
        chaosPercentResistance = stats1.chaosPercentResistance + stats2.chaosPercentResistance;
    }
    //public float movementSpeed;
}
