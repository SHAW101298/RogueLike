using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public State currentState;

    public State_Idle idle;
    public State_Chase chase;


    private void Update()
    {
        currentState.Do();
    }
    public void ChangeState(State newState)
    {
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
