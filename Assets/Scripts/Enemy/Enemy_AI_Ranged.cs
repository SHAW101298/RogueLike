using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_Ranged : Enemy_AI
{
    public State_Aiming aiming;
    public State_Attack_Ranged attackRanged;
    public State_Reloading reloading;
    [Space(10)]
    public Enemy_Weapon weapon;

    [SerializeField] Vector3 dest;


    private void Start()
    {
        if(NetworkObject.IsOwner == false)
        {
            agent.enabled = false;
            this.enabled = false;
        }
        else
        {
            // Makes Enemies move slightly out of spawn positions
            float x = Random.Range(-1.5f, 1.5f);
            float z = Random.Range(-1.5f, 1.5f);
            Vector3 randomPos = new Vector3(x, 0, z);
            //Debug.Log("Random = " + randomPos);
            randomPos += gameObject.transform.position;
            agent.SetDestination(randomPos);
            //Debug.Log("Current pos = " + gameObject.transform.position + "\nDEST = " + agent.destination + " \nRandomPos = " + randomPos);
        }
    }
    public override void ActivateAI()
    {
        //Debug.Log("Nothing Happening here");
        ChangeState(idle);
    }

    private void Update()
    {
        currentState.Do();
        dest = agent.destination;
    }
    public override bool CloseEnoughToAttack(PlayerData player)
    {
        if (weapon.isReloading == true)
        {
            return false;
        }

        attack.SetData(player);
        ChangeState(aiming);
        //Debug.Log("Changing State to Aiming");
        return true;
    }
    public void PlayerTooClose()
    {

    }
}
