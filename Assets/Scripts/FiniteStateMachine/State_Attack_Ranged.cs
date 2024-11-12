using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Ranged : State_Attack
{
    [Header("Ref")]
    public GameObject projectile;
    public float projectileSpeed;
    public Transform projectileStartPosition;

    public bool requiresCharging;
    public float chargeTime;
    float chargeTimer;

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

        if(requiresCharging == true)
        {
            chargeTimer -= chargeTime;
        }

    }
    public override void Do()
    {
        if(requiresCharging == true)
        {
            chargeTimer -= Time.deltaTime;

            if(chargeTimer <= 0)
            {
                ConfirmShot();
            }
        }
    }

    public override void Exit()
    {

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
    void SendBullet()
    {

        GameObject GO_Bullet = Instantiate(projectile);
        GO_Bullet.transform.position = projectileStartPosition.position;
    }
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
            SendBullet();
            // Set Bullet Data
        }
    }
    
}
