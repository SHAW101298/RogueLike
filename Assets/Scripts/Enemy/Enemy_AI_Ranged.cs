using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_Ranged : Enemy_AI_Base
{
    [Header("References")]
    public NavMeshAgent agent;
    public EnemyData data;

    [Header("Data")]
    public ENUM_AIState state;
    public float attackDistance;
    public float chaseDistance;

    [Header("Idle Data")]
    float distanceCheckTimer;
    public float distanceCheckDelay;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner == false)
            return;

        ActAccordingToState();

    }
    public override void ActivateAI()
    {
        throw new System.NotImplementedException();
    }
    void ActAccordingToState()
    {
        switch(state)
        {
            case ENUM_AIState.Inactive:
                STATE_Inactive();
                break;
            case ENUM_AIState.Idle:
                STATE_Idle();
                break;
            case ENUM_AIState.Moving:
                STATE_Moving();
                break;
            case ENUM_AIState.Attacking:
                STATE_Attacking();
                break;
            case ENUM_AIState.Dying:
                STATE_Dying();
                break;
            default:
                break;
        }
    }

    void STATE_Inactive()
    {

    }
    void STATE_Idle()
    {
        distanceCheckTimer -= Time.deltaTime;
        if(distanceCheckTimer < 0)
        {
            distanceCheckTimer = distanceCheckDelay;
            float dist = PlayerList.Instance.GetDistanceToClosestPlayer(transform.position);
            if(dist < chaseDistance)
            {

            }
        }
    }
    void STATE_Moving()
    {

    }
    void STATE_Attacking()
    {

    }
    void STATE_Dying()
    {

    }
    void TryToChangeState(ENUM_AIState newState)
    {
        if (state == newState)
            return;

        state = newState;
    }
    void InterruptCurrentState(ENUM_AIState newState)
    {

    }
}
