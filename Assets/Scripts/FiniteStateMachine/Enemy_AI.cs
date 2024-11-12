using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : Enemy_AI_Base
{
    public State currentState;

    public State_Idle idle;
    public State_Chase chase;
    public State_Attack attack;


    private void Update()
    {
        currentState.Do();
    }
    public void ChangeState(State newState)
    {
        //Debug.Log("Changing state from " + currentState + "to"  + newState);
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void PlayersEnteredRoom()
    {
        PlayerData closestPlayer = PlayerList.Instance.GetClosestPlayer(transform.position);
        chase.SetData(closestPlayer);

        ChangeState(chase);
    }
    public void PlayerLeftChaseDistance()
    {
        ChangeState(idle);
    }
    public void CloseEnoughToAttack(PlayerData player)
    {
        attack.SetData(player);
        ChangeState(attack);
    }

    public override void ActivateAI()
    {
        PlayersEnteredRoom();
    }






    // In idle state
    // Just chillin
    // Player enters room
    // Im told to chase him
    // I chase the closest or random player
    // When im close enough i attack
    // If player is not in line of sight, change target or get closer
    // If player dead change target
    // If im dead i die
}
