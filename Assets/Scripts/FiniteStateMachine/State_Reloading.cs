using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Reloading : State
{
    [SerializeField] Enemy_Weapon weapon;

    public override void Do()
    {
        if(weapon.isReloading == true)
        {
            return;
        }
        else
        {
            ai.ChangeState(ai.chase);
        }
    }

    public override void Enter()
    {
        ai.agent.SetDestination(ai.transform.position);
    }

    public override void Exit()
    {

    }

    public override void FixedDo()
    {

    }

    public override string ToString()
    {
        return "Reloading";
    }
}
