using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blessing_Gonzales : Blessing_Base
{
    [Tooltip("Increase movement by % value")]
    public float movementSpeed;
    public Effect_MovementSpeedIncreaseFlat template;
    public override void Apply()
    {
        PlayerData player = GetComponentInParent<PlayerData>();
        Effect_MovementSpeedIncreaseFlat effect = Instantiate(template);
        effect.value = movementSpeed;
        effect.isPermanent = true;
        effect.canBeRemoved = false;
        effect.canBeStacked = true;
        effect.isVisible = false;
        player.effects.AddEffect(effect);
    }
    public override string GetDescription()
    {
        string text = "Increase movement speed by " + movementSpeed.ToString();
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
