using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect_Base : MonoBehaviour
{
    public Sprite icon;
    public float remainingTime;
    public float timer;
    public bool isPermanent;
    public bool isVisible;
    public bool canBeRemoved;
    public bool canBeStacked;

    public abstract void Remove();
    public abstract void Apply();
    public abstract void Refresh();
}
