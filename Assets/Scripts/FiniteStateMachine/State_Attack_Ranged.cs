using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Ranged : State_Attack
{
    [Header("Ref")]
    public BulletData projectilePrefab;
    public float projectileSpeed;
    public Transform projectileStartPosition;

    public bool requiresCharging;
    public float chargeTime;
    [SerializeField] float chargeTimer;

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
                chargeTimer = chargeTime;
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
    void SendBullet(Vector3 dir)
    {
        GameObject GO_Bullet = Instantiate(projectilePrefab.gameObject);
        GO_Bullet.transform.position = projectileStartPosition.position;


        BulletData newBullet = GO_Bullet.GetComponent<BulletData>();
        FillBulletData(dir, newBullet);

        ProjectileBehaviour behaviour = GO_Bullet.GetComponent<ProjectileBehaviour>();
        behaviour.direction = dir;
    }

    private void FillBulletData(Vector3 dir, BulletData newBullet)
    {
        newBullet.bulletInfo = projectilePrefab.bulletInfo;
        newBullet.projectileBehaviour.owningFaction = ENUM_Faction.enemy;
        newBullet.projectileBehaviour.direction = dir * projectileSpeed;
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


}
