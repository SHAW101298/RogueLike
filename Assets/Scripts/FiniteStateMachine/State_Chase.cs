using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chase : State
{
    PlayerData chasedPlayer;
    float timer;
    [SerializeField] float distanceCheckTime;
    public float chaseDistance;

    
    public override void Enter()
    {
        if(chasedPlayer == null)
        {
            if(ai.target == null)
            {
                chasedPlayer = PlayerList.Instance.GetClosestPlayer(transform.position);
                ai.target = chasedPlayer;
            }
            else
            {
                chasedPlayer = ai.target;
            }
        }
        // Set animation data maybe
    }
    public override void Do()
    {
        agent.SetDestination(chasedPlayer.transform.position);
        if(CheckIfCloseEnoughToAttack() == true)
        {
            return;
        }
        if(CheckIfPlayerIsTooNear() == true)
        {
            ai.ChangeState(ai.run);
            return;
        }


        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = distanceCheckTime;
            CheckIfPlayerRanAway();
        }
        

    }
    public override void Exit()
    {
        chasedPlayer = null;
        agent.SetDestination(transform.position);
    }
    public override void FixedDo()
    {
    }

    public void SetData(PlayerData player)
    {
        chasedPlayer = player;
    }
    void CheckIfPlayerRanAway()
    {
        PlayerData currentClosestPlayer = PlayerList.Instance.GetClosestPlayer(transform.position);
        float currentClosestDistance = Vector3.Distance(transform.position, currentClosestPlayer.transform.position);

        bool reset = false;
        if (agent.remainingDistance > chaseDistance)
        {
            reset = true;
        }
        if(currentClosestDistance < agent.remainingDistance && currentClosestDistance < chaseDistance)
        {
            reset = false;
        }
        if(reset == true)
        {
            ai.PlayerLeftChaseDistance();
        }
    }
    bool CheckIfCloseEnoughToAttack()
    {
        float dist = Vector3.Distance(transform.position, chasedPlayer.transform.position);
        bool decision = false;

        if (dist <= ai.attack.attackDistance)
        {
            agent.SetDestination(transform.position);
            //Debug.Log("Remaining Distance is " + agent.remainingDistance);
            decision = ai.CloseEnoughToAttack(chasedPlayer);
        }
        return decision;
    }
    bool CheckIfPlayerIsTooNear()
    {
        float dist = Vector3.Distance(transform.position, chasedPlayer.transform.position);
        //Debug.Log("dist = " + dist);
        if (dist < ai.run.minDistanceBetweenTarget)
        {
            //Debug.Log("Remaining Distance = " + agent.remainingDistance);
            return true;
        }
        return false;
    }
}
