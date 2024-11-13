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


    void CheckIfPlayerIsTooFar()
    {
        if (Vector3.Distance(transform.position, attackTarget.transform.position) > attackDistance)
        {
            ai.PlayersEnteredRoom();
        }
    }


    /*
    void ConfirmShot()
    {
        Vector3 dir = Tools.Direction(attackTarget.transform.position, projectileStartPosition.position);
        bool visiblePlayer = Tools.RaycastOnLayer(projectileStartPosition.position, dir, 100f, playerLayer);
        if (visiblePlayer == false)
        {
            PlayerData newTarget;
            for (int i = 0; i < PlayerList.Instance.players.Count; i++)
            {
                newTarget = PlayerList.Instance.GetPlayer(i);
                dir = Tools.Direction(newTarget.transform.position, projectileStartPosition.position);
                dir.Normalize();
                visiblePlayer = Tools.RaycastOnLayer(projectileStartPosition.position, dir, 100f, playerLayer);

                if (visiblePlayer == true)
                {
                    attackTarget = newTarget;
                    break;
                }
            }
        }

        if (visiblePlayer == false)
        {
            // ABORT
        }
        else
        {
            SendBullet(dir);
            // Set Bullet Data
        }
    }
    */


}
