using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Aiming : State
{
    [SerializeField] float aimingTime;
    [SerializeField] bool aiming;
    float aimTimer;
    public override void Enter()
    {
        aiming = true;
        aimingTime = 0;
        // Some enemies will shoot without warning
        if(aimingTime == 0)
        {
            ai.ChangeState(ai.attack);
        }

        ai.gameObject.transform.LookAt(ai.attack.attackTarget.transform.position);
    }

    public override void Do()
    {
        aimTimer += Time.deltaTime;
        if(aimTimer <= aimingTime)
        {
            aimTimer = 0;
            ai.ChangeState(ai.attack);
        }
    }

    public override void Exit()
    {
        aimTimer = 0;
    }

    public override void FixedDo()
    {

    }

}
