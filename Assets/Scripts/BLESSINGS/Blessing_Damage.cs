using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Damage : Blessing_Base
{
    [Tooltip("Increase damage by % value")]
    public float damage;
    public Effect_DamageIncrease template;
    public override void Apply()
    {
        PlayerData player = GetComponentInParent<PlayerData>();
        Effect_DamageIncrease effect = Instantiate(template);
        effect.value = damage;
        effect.isPermanent = true;
        effect.canBeRemoved = false;
        effect.canBeStacked = true;
        effect.isVisible = false;
        player.effects.AddEffect(effect);
    }
    public override string GetDescription()
    {
        string text = "Increase damage by " + damage + " %";
        return text;
    }

    void BlessingLogic()
    {

    }
    private void Update()
    {
        BlessingLogic();
    }
}
