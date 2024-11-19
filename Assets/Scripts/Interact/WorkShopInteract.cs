using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkShopInteract : InteractableBase
{
    public override void Interact(PlayerData data)
    {
        data.ui.ShowWorkShopWindow();
    }
}
