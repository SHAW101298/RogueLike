using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Chase : State
{
    PlayerData chasedPlayer;
    float distancetimer;
    float destinationtimer;
    [SerializeField] float distanceCheckTime;
    [SerializeField] float destinationSetTime;
    public float chaseDistance;
    public LayerMask unitsLayer;
    [SerializeField] NavMeshPath path;


    private void Start()
    {
        path = new NavMeshPath();
    }

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
                agent.SetDestination(chasedPlayer.transform.position);
            }
        }
        else
        {
            agent.SetDestination(chasedPlayer.transform.position);
        }
        // Set animation data maybe
    }
    public override void Do()
    {
        //agent.SetDestination(chasedPlayer.transform.position);
        //Debug.Log("Is path pending ? " + agent.pathPending + "\nPath Status is " + agent.pathStatus.ToString());
        AttackRangeCheck();
        PlayerRunningAwayCheck();
    }

    private void PlayerRunningAwayCheck()
    {
        distancetimer -= Time.deltaTime;
        if (distancetimer <= 0)
        {
            distancetimer = distanceCheckTime;
            CheckIfPlayerRanAway();
        }
    }

    private void AttackRangeCheck()
    {
        destinationtimer += Time.deltaTime;
        if (destinationtimer >= destinationSetTime)
        {
            NavMesh.CalculatePath(ai.transform.position, chasedPlayer.transform.position, NavMesh.AllAreas, path);
            agent.SetPath(path);
            destinationtimer = 0;


            if (CheckIfCloseEnoughToAttack() == true)
            {
                //agent.SetDestination(transform.position);
                return;
            }
        }
    }

    public override void Exit()
    {
        //Debug.Log("Leaving Chase");
        chasedPlayer = null;
        //Debug.Log("Current DEST = " + agent.destination);
        agent.SetDestination(ai.transform.position);
        //Debug.Log("New Dest = " + agent.destination);
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
        Debug.Log("Check if player Ran Away");
        PlayerData currentClosestPlayer = PlayerList.Instance.GetClosestPlayer(transform.position);
        float currentClosestDistance = Vector3.Distance(transform.position, currentClosestPlayer.transform.position);

        bool reset = false;
        float remainingDistance = Vector3.Distance(agent.transform.position, chasedPlayer.transform.position);
        if (remainingDistance > chaseDistance)
        {
            //Debug.Log("Remaining Distance = " + remainingDistance + " | Chase Distance = " + chaseDistance);
            reset = true;
        }
        if(currentClosestDistance < remainingDistance && currentClosestDistance < chaseDistance)
        {
            //Debug.Log("Current closest = " + currentClosestDistance);
            reset = false;
        }
        if(reset == true)
        {
            Debug.Log("Player Ran Away");
            ai.PlayerLeftChaseDistance();
        }
    }
    bool CheckIfCloseEnoughToAttack()
    {
        Debug.Log("Check if Close Enough to Attack");
        float dist = Vector3.Distance(transform.position, chasedPlayer.transform.position);
        Debug.Log("Dist = " + dist);
        bool decision = false;

        if (dist <= ai.attack.attackDistance)
        {
            //Debug.Log("Checking distance");
            Vector3 dir = chasedPlayer.GetShootTarget() - ai.data.rayCastPosition.position;
            RaycastHit info;
            if(Physics.Raycast(ai.data.rayCastPosition.position, dir, out info, 100f, unitsLayer) == true)
            {
                if(info.collider.gameObject.CompareTag("Player"))
                {
                    decision = ai.CloseEnoughToAttack(chasedPlayer);
                }
                /*
                //Debug.Log("We hit a unit " + info.collider.gameObject.name);
                if(info.collider.gameObject.GetComponent<UnitData>().faction == ENUM_Faction.player)
                {
                    //Debug.Log("Its a player");
                    // Checking if enemy is reloading
                    decision = ai.CloseEnoughToAttack(chasedPlayer);
                }
                */
            }
            //Debug.Log("Remaining Distance is " + agent.remainingDistance);
            //decision = ai.CloseEnoughToAttack(chasedPlayer);
        }
        Debug.Log("Decision = " + decision);
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
    public override string ToString()
    {
        return "Chasing";
    }
}
