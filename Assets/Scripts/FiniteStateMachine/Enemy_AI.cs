using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy_AI : NetworkBehaviour
{
    public EnemyData data;
    public NavMeshAgent agent;
    [Space(10)]
    public State currentState;
    public State_Idle idle;
    public State_Chase chase;
    public State_Run run;
    public State_Attack attack;

    public PlayerData target;
    [SerializeField] float rotationSpeed = 20;
    Vector3 dirRotation;
    Quaternion lookRotation;

    


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
        target = closestPlayer;
        ChangeState(chase);
    }
    public void PlayerLeftChaseDistance()
    {
        ChangeState(idle);
        target = null;
    }
    public abstract bool CloseEnoughToAttack(PlayerData player);
    public abstract void ActivateAI();

    public void RotateTowardsTarget()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        dirRotation = Tools.Direction(targetPos, transform.position);
        lookRotation = Quaternion.LookRotation(dirRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime *rotationSpeed);
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
