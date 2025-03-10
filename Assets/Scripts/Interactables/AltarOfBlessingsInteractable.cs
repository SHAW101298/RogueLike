using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarOfBlessingsInteractable : InteractableBase
{
    public AltarOfBlessings altarOfBlessings;
    public Transform positionToPlaceBlessing;
    public bool isActive;
    public override void Interact(PlayerData data)
    {
        if (isActive == false)
            return;

        data.GiveBlessing(altarOfBlessings.blessing);
        altarOfBlessings.DisableAltar();
    }

}
