using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Enemy_AI_Base : NetworkBehaviour
{
    public abstract void ActivateAI();
}
