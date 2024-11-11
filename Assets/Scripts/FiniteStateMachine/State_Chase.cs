using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chase : State
{
    PlayerData chasedPlayer;
    float timer;
    [SerializeField] float distanceCheckTime;
    [SerializeField] float chaseDistance;
    [SerializeField] float attackDistance;
    public override void Enter()
    {
        // Set animation data maybe
    }
    public override void Do()
    {
        agent.SetDestination(chasedPlayer.transform.position);

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
        float distance = Vector3.Distance(transform.position, currentClosestPlayer.transform.position);

        if(currentClosestPlayer != chasedPlayer)
        {
            chasedPlayer = currentClosestPlayer;
            if(distance > chaseDistance)
            {
                ai.PlayerLeftChaseDistance();
            }
        }
    }
}
