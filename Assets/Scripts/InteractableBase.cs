using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_InteractableType
{
    gunPickup,
    door
}
public abstract class InteractableBase : MonoBehaviour
{
    public ENUM_InteractableType interactableType;
    public abstract void Interact(PlayerData data);
}
