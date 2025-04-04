using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Enemy_Weapon : NetworkBehaviour
{
    [Header("Damage Data")]
    public DamageData damage;
    public ElementalTable afflictionModifiers;
    public float critChance;
    public float critMultiplier;
    public float afflictionChance;
    public int punchThrough;
    [Header("Ref")]
    [SerializeField] BulletData projectilePrefab;
    [SerializeField] Transform projectileSpawnPosition;
    [SerializeField] Enemy_AI_Ranged ai;
    [Header("Limiters")]
    [SerializeField] int magazineCurrent;
    [SerializeField] int magazineMax;
    [SerializeField] float projectileSpeed;
    [SerializeField] float aimCorrection = 1f;
    public bool isReloading { get; private set;}
    [SerializeField] float reloadTime;
    [SerializeField] float timeBetweenShots;
    float timer;
    float shotTimer;


    private void Update()
    {
        if(isReloading == true)
        {
            timer += Time.deltaTime;
            if(timer >= reloadTime)
            {
                timer = 0;
                magazineCurrent = magazineMax;
                isReloading = false;
            }
        }
    }
    public void Shoot()
    {
        if (magazineCurrent <= 0)
        {
            return;
        }
        if(shotTimer < timeBetweenShots)
        {
            shotTimer += Time.deltaTime;
            return;
        }
        shotTimer = 0;
        magazineCurrent--;
        SendBullet();
        CheckAmmo();
    }
    void SendBullet()
    {
        GameObject GO_Bullet = Instantiate(projectilePrefab.gameObject);
        GO_Bullet.transform.position = projectileSpawnPosition.position;
        BulletData newBullet = GO_Bullet.GetComponent<BulletData>();
        Vector3 dir = Tools.Direction(GetCorrectedPlayerPosition(), projectileSpawnPosition.position);
        FillBulletData(dir, newBullet);
        SendBullet_ClientRPC(dir);
    }
    Vector3 GetCorrectedPlayerPosition()
    {
        Vector3 dir = ai.attack.attackTarget.GetShootTarget();
        dir += ai.attack.attackTarget.movement.GetMoveDir() * aimCorrection;
        return dir;
    }
    [ClientRpc]
    public void SendBullet_ClientRPC(Vector3 dir)
    {
        // Host nie musi wytwarza� kopii pocisku
        if(NetworkManager.Singleton.IsHost == true)
        {
            //Debug.Log("IM HOST DUH");
            return;
        }

        GameObject GO_Bullet = Instantiate(projectilePrefab.gameObject);
        GO_Bullet.transform.position = projectileSpawnPosition.position;
        BulletData newBullet = GO_Bullet.GetComponent<BulletData>();
        FillBulletData(dir, newBullet);
    }
    private void FillBulletData(Vector3 dir, BulletData newBullet)
    {
        //newBullet.bulletInfo = projectilePrefab.bulletInfo;
        newBullet.projectileBehaviour.owningFaction = ENUM_Faction.enemy;
        newBullet.owningUnit = ai.data;
        dir *= projectileSpeed;

        newBullet.bulletInfo.damageData = new List<DamageData>();
        newBullet.bulletInfo.damageData.Add(damage);
        newBullet.bulletInfo.damageModifierWhenAfflicted = afflictionModifiers;
        newBullet.bulletInfo.critChance = critChance;
        newBullet.bulletInfo.critDamageMultiplier = critMultiplier;
        newBullet.bulletInfo.afflictionChance = afflictionChance;
        newBullet.bulletInfo.punchThrough = punchThrough;

        newBullet.projectileBehaviour.direction = dir;
        //Debug.Log("dir * speed = " + dir);
    }
    void CheckAmmo()
    {
        if (magazineCurrent <= 0)
        {
            isReloading = true;
            ai.chase.SetData(ai.attack.attackTarget);
            ai.ChangeState(ai.reloading);
            Debug.LogWarning("Change state to something different than chase or attack");
        }
    }
    public void OrderReaload()
    {
        isReloading = true;
    }

    

}
