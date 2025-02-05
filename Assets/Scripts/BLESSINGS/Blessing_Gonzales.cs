using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blessing_Gonzales : Blessing_Base
{
    [Tooltip("Increase movement by % value")]
    public float movementSpeed;
    public Gonzales_Effect effect;
    public override void Apply()
    {
        PlayerData player = GetComponentInParent<PlayerData>();
        effect.
    }
    public override string GetDescription()
    {
        string text = "Increase movement speed by " + movementSpeed + " %";
        return text;
    }
}
