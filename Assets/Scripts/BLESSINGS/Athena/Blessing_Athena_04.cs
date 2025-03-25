using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_04 : Blessing_Base
{
    [SerializeField] float damageReduction;
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
        string text = "On Weapon swap decrase taken damage for " + reductionTime + " seconds, by " + damageReduction + "%.";
        return text;
    }

    public override void Remove()
    {
        if(activeBlessing == true)
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
        player.bonusStats.percentDamageResistance += damageReduction;
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
        player.bonusStats.percentDamageResistance -= damageReduction;
        player.RecalculateStats();
    }
    private void Update()
    {
        if(activeBlessing == true)
        {
            timer += Time.deltaTime;
            if(timer >= reductionTime)
            {
                BlessingLogicTimeOut();
            }
        }
    }

}
