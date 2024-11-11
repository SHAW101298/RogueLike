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
        // Set animation data maybe
    }
    public override void Do()
    {
        agent.SetDestination(chasedPlayer.transform.position);

        CheckIfCloseEnoughToAttack();

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
    void CheckIfCloseEnoughToAttack()
    {
        float dist = Vector3.Distance(transform.position, chasedPlayer.transform.position);

        if (dist <= ai.attack.attackDistance)
        {
            Debug.Log("Remaining Distance is " + agent.remainingDistance);
            ai.CloseEnoughToAttack(chasedPlayer);
        }
    }
}
