using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class State_Aiming : State
{
    [SerializeField] float aimingTime;
    [SerializeField] bool aiming;
    float aimTimer;

    Quaternion lookRotation;
    Vector3 dirRotation;
    public override void Enter()
    {
        aiming = true;
        // Some enemies will shoot without warning
        if(aimingTime == -1)
        {
            ai.ChangeState(ai.attack);
        }
    }

    public override void Do()
    {
        aimTimer += Time.deltaTime;

        ai.RotateTowardsTarget();

        if (aimTimer >= aimingTime)
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
    public override string ToString()
    {
        return "Aiming";
    }
}
