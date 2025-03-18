using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chase : State
{
    PlayerData chasedPlayer;
    float distancetimer;
    float destinationtimer;
    [SerializeField] float distanceCheckTime;
    [SerializeField] float destinationSetTime;
    public float chaseDistance;
    public LayerMask unitsLayer;

    
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
        destinationtimer += Time.deltaTime;
        if(destinationtimer >= destinationSetTime)
        {
            agent.SetDestination(chasedPlayer.transform.position);
            destinationtimer = 0;

            if (CheckIfCloseEnoughToAttack() == true)
            {
                //agent.SetDestination(transform.position);
                return;
            }
        }

        /*
        if(CheckIfPlayerIsTooNear() == true)
        {
            ai.ChangeState(ai.run);
            return;
        }
        */


        distancetimer -= Time.deltaTime;
        if(distancetimer <= 0)
        {
            distancetimer = distanceCheckTime;
            CheckIfPlayerRanAway();
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
            ai.PlayerLeftChaseDistance();
        }
    }
    bool CheckIfCloseEnoughToAttack()
    {
        float dist = Vector3.Distance(transform.position, chasedPlayer.transform.position);
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
