using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Blessing_Base : MonoBehaviour
{
    public int id;
    public string title;
    public string description;

    public abstract void Apply();
    public abstract string GetDescription();
}
