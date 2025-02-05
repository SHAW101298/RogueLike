using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blessing_Gonzales : Blessing_Base
{
    [Tooltip("Increase movement by % value")]
    public float movementSpeed;
    public Effect_MovementSpeedDecrease template;
    public override void Apply()
    {
        PlayerData player = GetComponentInParent<PlayerData>();
        Effect_MovementSpeedDecrease effect = Instantiate(template);
        effect.value = movementSpeed;
        player.effects.AddEffect(effect);
    }
    public override string GetDescription()
    {
        string text = "Increase movement speed by " + movementSpeed + " %";
        return text;
    }
}
