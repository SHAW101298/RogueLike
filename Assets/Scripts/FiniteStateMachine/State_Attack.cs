using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State
{
    PlayerData attackTarget;
    public float attackDistance = 4;
    public override void Enter()
    {
        agent.SetDestination(transform.position);
    }
    public override void Do()
    {
        CheckIfPlayerIsTooFar();
    }

    public override void Exit()
    {

    }

    public override void FixedDo()
    {

    }
    public void SetData(PlayerData player)
    {
        attackTarget = player;
    }
    void CheckIfPlayerIsTooFar()
    {
        if(Vector3.Distance(transform.position, attackTarget.transform.position) > attackDistance)
        {
            ai.PlayersEnteredRoom();
        }
    }
}
