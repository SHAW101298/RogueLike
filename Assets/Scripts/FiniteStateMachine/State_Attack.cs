using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Attack : State
{
    public float attackDistance;
    protected PlayerData attackTarget;
    public abstract void SetData(PlayerData player);
}