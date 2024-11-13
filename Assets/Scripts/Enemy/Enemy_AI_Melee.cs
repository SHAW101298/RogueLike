using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public enum ENUM_AIState
{
    Inactive,
    Idle,
    Moving,
    Attacking,
    Dying
}
public enum ENUM_StateProgress
{
    work,
    finishing,
    end
}

public class Enemy_AI_Melee : Enemy_AI
{


    [SerializeField] float attackRange;
    [SerializeField] PlayerData closestPlayer;
    [SerializeField] ENUM_AIState aiState;
    [SerializeField] ENUM_StateProgress stateProgress;

    [Header("Debug")]
    [SerializeField] Transform movePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner == false)
            //return;

        ActAccordingToState();

        MoveTowardsPlayers();
    }

    public override void ActivateAI() 
    {
        AttemptStateChange(ENUM_AIState.Idle);
    }
    void MoveTowardsPlayers()
    {
        agent.SetDestination(movePosition.position);
        // Find Closest Player in range of 100
    }

    void ActAccordingToState()
    {
        switch(aiState)
        {
            case ENUM_AIState.Inactive:
                Inactive();
                break;
            case ENUM_AIState.Idle:
                Idle();
                break;
            case ENUM_AIState.Moving:
                Moving();
                break;
            case ENUM_AIState.Attacking:
                Attacking();
                break;
            case ENUM_AIState.Dying:
                Dying();
                break;
            default:
                break;
        }
    }
    bool AttemptStateChange(ENUM_AIState newstate)
    {
        if(newstate == aiState)
        {
            return false;
        }

        if(stateProgress == ENUM_StateProgress.end)
        {
            aiState = newstate;
            stateProgress = ENUM_StateProgress.work;
            return true;
        }
        else
        {
            if (aiState == ENUM_AIState.Moving)
            {
                aiState = newstate;
                stateProgress = ENUM_StateProgress.work;
                return true;
            }
        }



        return false;
    }

    void Inactive()
    {
        return;
    }
    void Idle()
    {
        return;
    }
    void Moving()
    {
        agent.SetDestination(closestPlayer.transform.position);
    }
    void Attacking()
    {

    }
    void Dying()
    {

    }

    public override void CloseEnoughToAttack(PlayerData player)
    {
        throw new System.NotImplementedException();
    }
}
