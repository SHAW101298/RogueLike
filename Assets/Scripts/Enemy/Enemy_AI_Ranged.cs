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

    public override void ActivateAI()
    {
        PlayersEnteredRoom();
        
    }

    private void Update()
    {
        currentState.Do();
    }
    public override void CloseEnoughToAttack(PlayerData player)
    {
        if (weapon.isReloading == true)
        {
            return;
        }

        attack.SetData(player);
        ChangeState(aiming);
    }

}
