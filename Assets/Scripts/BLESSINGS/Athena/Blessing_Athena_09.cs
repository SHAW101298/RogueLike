using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_09 : Blessing_Base
{
    // Po uzyciu umiejetnosci zwiekszenie szybkosci strzelania
    [SerializeField] float fireRateIncrease;
    [SerializeField] float effectTime;
    [SerializeField] float cooldown;
    float cooldownTimer;
    float activeTimer;
    bool regenerating;
    bool activeBlessing = false;
    public override void Apply()
    {
        GetParent();
        player.events.OnAbilityUseEvent.AddListener(BlessingLogicOnAbilityUse);
    }

    public override string GetDescription()
    {
        string text = "Increase Rate of Fire for by " + fireRateIncrease +"% On Abiliy Use once every " + cooldown + " seconds.";
        return text;
    }

    public override void Remove()
    {
        if(activeBlessing == true)
        {
            player.bonusStats.gunFireRate -= fireRateIncrease;
        }
        player.events.OnAbilityUseEvent.RemoveListener(BlessingLogicOnAbilityUse);
    }
    public void BlessingLogicOnAbilityUse()
    {
        if (activeBlessing == true)
            return;
        if (regenerating == true)
            return;

        player.bonusStats.gunFireRate += fireRateIncrease;
        activeBlessing = true;
    }

    private void Update()
    {
        if(activeBlessing == true)
        {
            activeTimer += Time.deltaTime;
            if (activeTimer >= effectTime)
            {
                activeBlessing = false;
                activeTimer = 0;
                regenerating = true;
            }
        }
        if (regenerating == true)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldown)
            {
                regenerating = false;
                cooldownTimer = 0;
            }
        }
    }
}