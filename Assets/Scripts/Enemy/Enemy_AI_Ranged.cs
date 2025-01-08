using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_Ranged : Enemy_AI
{
    public State_Aiming aiming;
    public State_Attack_Ranged attackRanged;
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
    }
    public override void ActivateAI()
    {
        PlayersEnteredRoom();
        
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
        return true;
    }
    public void PlayerTooClose()
    {

    }

}
