using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_08 : Blessing_Base
{
    // Po uzyciu umiejetnosci regeneracja tarczy
    [SerializeField] float regenerationPercent;
    [SerializeField] float cooldown;
    float timer;
    bool regenerating;
    public override void Apply()
    {
        GetParent();
        player.events.OnAbilityUseEvent.AddListener(BlessingLogicOnAbilityUse);
    }

    public override string GetDescription()
    {
        string text = "Regenerate " + regenerationPercent + "% of shield On Abiliy Use once every " + cooldown + " seconds.";
        return text;
    }

    public override void Remove()
    {
        player.events.OnAbilityUseEvent.RemoveListener(BlessingLogicOnAbilityUse);
    }
    public void BlessingLogicOnAbilityUse()
    {
        if (regenerating == true)
            return;

        player.AddCurrentShieldPercent(regenerationPercent);
        regenerating = true;
    }

    private void Update()
    {
        if(regenerating == true)
        {
            timer += Time.deltaTime;
            if(timer >= cooldown)
            {
                regenerating = false;
                timer = 0;
            }
        }
    }
}
