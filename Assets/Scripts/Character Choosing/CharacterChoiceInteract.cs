using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoiceInteract : InteractableBase
{
    public override void Interact(PlayerData data)
    {
        data.ui.ShowCharacterSelector();
    }
}
