using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect_Base : MonoBehaviour
{
    public Sprite icon;
    public float remainingTime;
    public float timer;
    public bool isPermanent;
    public bool canBeRemoved;

    public abstract void Remove();
    public abstract void Apply();
    public abstract void Refresh();
}
