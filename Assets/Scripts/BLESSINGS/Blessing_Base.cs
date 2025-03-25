using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Blessing_Base : MonoBehaviour
{
    public int id;
    public string title;
    public string description;
    [SerializeField] protected PlayerData player;
    [SerializeField] Blessing_Base counterPart;
    public bool isCorrupted;

    public abstract void Apply();
    public abstract void Remove();
    public abstract string GetDescription();
    public Blessing_Base GetCounterPart()
    {
        return counterPart;
    }
}
