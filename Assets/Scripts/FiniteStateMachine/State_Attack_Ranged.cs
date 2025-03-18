using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Ranged : State_Attack
{
    [Header("Ref")]
    [SerializeField] Enemy_Weapon weapon;

    public Transform projectileStartPosition;
    [SerializeField] LayerMask playerLayer;


    /*
     How attacking goes ?
    Enemy notices player
    Is close enough to shoot
    ENTER()
    check if attack requires charging, if yes start charging
    */
    public override void Enter()
    {
        agent.SetDestination(transform.position);

    }
    public override void Do()
    {
        ai.RotateTowardsTarget();
        weapon.Shoot();
    }

    public override void Exit()
    {
        attackTarget = null;
        weapon.OrderReaload();
    }

    public override void FixedDo()
    {

    }
    public override void SetData(PlayerData player)
    {
        attackTarget = player;
    }

    public override string ToString()
    {
        return "AttackRanged";
    }

}
