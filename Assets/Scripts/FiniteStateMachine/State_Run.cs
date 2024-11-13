using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Run : State
{
    public float minDistanceBetweenTarget;
    public Vector3 desiredPos;

    public override void Enter()
    {
        Debug.Log("Entering Run");
        Vector3 dir = Tools.Direction(transform.position, ai.target.transform.position);
        dir *= (minDistanceBetweenTarget + 2);
        dir = dir + transform.position;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(dir,out hit, 4f, NavMesh.AllAreas) == true)
        {
            desiredPos = hit.position;
            agent.SetDestination(desiredPos);
        }

    }

    public override void Do()
    {
        ai.RotateTowardsTarget();

        if(agent.remainingDistance <= 0.5f)
        {
            ai.ChangeState(ai.chase);
        }
    }

    public override void Exit()
    {
        
    }

    public override void FixedDo()
    {
    }
}
