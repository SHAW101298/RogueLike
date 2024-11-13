using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Ranged : State_Attack
{
    [Header("Ref")]
    public BulletData projectilePrefab;
    public float projectileSpeed;
    public Transform projectileStartPosition;
    [SerializeField] LayerMask playerLayer;

    [Space(10)]
    [SerializeField] int currentAmmo;
    [SerializeField] int maxAmmo;

    [Header("Charging")]
    public bool requiresCharging;
    public float chargeTime;
    [SerializeField] float chargeTimer;
    
    [Header("Reload")]
    [SerializeField] float reloadTime;
    float reloadTimer;
    [SerializeField] bool reloading;

    [Header("TimeBetweenShots")]
    [SerializeField] float timeBetweenShots;
    float timerBetween;
    

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
        if (reloading == true)
        {
            Reloading();
            return;
        }

        if (requiresCharging == true)
        {
            chargeTimer -= Time.deltaTime;

            if(chargeTimer <= 0)
            {
                //ConfirmShot();
                Shoot();
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
    }

    private void FillBulletData(Vector3 dir, BulletData newBullet)
    {
        newBullet.bulletInfo = projectilePrefab.bulletInfo;
        newBullet.projectileBehaviour.owningFaction = ENUM_Faction.enemy;
        dir *= projectileSpeed;

        newBullet.projectileBehaviour.direction = dir;
        //Debug.Log("dir * speed = " + dir);
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
    void Shoot()
    {
        //Debug.Log("Shoot");
        if(maxAmmo > 1)
        {
            timerBetween -= Time.deltaTime;
            if(timerBetween <= 0)
            {
                timerBetween += timeBetweenShots;
                Vector3 dir = Tools.Direction(attackTarget.transform.position, projectileStartPosition.position);
                dir.Normalize();
                SendBullet(dir);
                currentAmmo--;
            }
            if(currentAmmo <= 0)
            {
                reloading = true;
                chargeTimer = chargeTime;
            }
        }
        else
        {
            Vector3 dir = Tools.Direction(attackTarget.transform.position, projectileStartPosition.position);
            dir.Normalize();
            SendBullet(dir);
            reloading = true;
            chargeTimer = chargeTime;
        }
    }

    private void Reloading()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0)
        {
            reloading = false;
            currentAmmo = maxAmmo;
        }
    }
}
