using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
{
    float timer;
    [SerializeField] float timerDelay = 2;
    public override void Enter()
    {
        
    }
    public override void Do()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = timerDelay;
            PlayerData closestPlayer = PlayerList.Instance.GetClosestPlayer(transform.position);
            if(Vector3.Distance(closestPlayer.transform.position, transform.position) < ai.chase.chaseDistance)
            {
                //ai.NotifyAboutPlayer(closestPlayer);
            }
        }
    }
    public override void FixedDo()
    {
        
    }
    public override void Exit()
    {
        timer = timerDelay;
    }
    public override string ToString()
    {
        return "Idle";
    }
}
