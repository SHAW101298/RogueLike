using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_05 : Blessing_Base
{
    [SerializeField] float damageIncrease;
    [SerializeField] float reductionTime;
    float timer = 0;
    bool activeBlessing = false;
    public override void Apply()
    {
        GetParent();
        player.events.OnWeaponSwapEvent.AddListener(BlessingLogicWeaponSwap);
    }

    public override string GetDescription()
    {
        string text = "On Weapon swap increase damage for " + reductionTime + " seconds, by " + damageIncrease + "%.";
        return text;
    }

    public override void Remove()
    {
        if (activeBlessing == true)
        {
            BlessingLogicTimeOut();
        }

        player.events.OnWeaponSwapEvent.RemoveListener(BlessingLogicWeaponSwap);
    }

    public void BlessingLogicWeaponSwap()
    {
        // Refresh
        if(activeBlessing == true)
        {
            timer = 0;
            return;
        }

        timer = 0;
        activeBlessing = true;
        player.bonusStats.damageModifier += damageIncrease;
        player.RecalculateStats();
    }
    public void BlessingLogicTimeOut()
    {
        if (activeBlessing == false)
        {
            return;
        }
        timer = 0;
        activeBlessing = false;
        player.bonusStats.damageModifier -= damageIncrease;
        player.RecalculateStats();
    }
    private void Update()
    {
        if (activeBlessing == true)
        {
            timer += Time.deltaTime;
            if (timer >= reductionTime)
            {
                BlessingLogicTimeOut();
            }
        }
    }
}
