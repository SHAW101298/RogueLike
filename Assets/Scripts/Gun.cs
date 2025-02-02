using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ENUM_GunType
{
    pistol,
    smg,
    shotgun,
    sniper,
    rocketLauncher
}


public class Gun : MonoBehaviour
{
    public int presetID;
    [Header("Data")]
    public GunStats baseStats;
    [Space(10)]
    public GunStats modifiedStats;

    public List<GunUpgradeBase> gunUpgrades;
    public GU_ChaoticUpgrade chaoticUpgrade;
    [Space(25)]
    public ENUM_GunType gunType;

    public int magazineCurrent;
    float reloadTimer;
    float shotTimer;
    bool reload;
    public LayerMask bulletMask;
    public bool canBeSwapped; // Use During Picking Up New Weapons

    [Header("Ref")]
    public BulletData projectilePrefab;
    public GameObject nozzle;
    public PlayerGunManagement gunManagement;
    public PlayerData playerData;

    [Header("Debug")]
    public bool ForceGun1 = true;

    public void EVENT_AppliedStatus()
    {

    }
    public void EVENT_ScoredCrit()
    {

    }
    

    // Start is called before the first frame update
    void Start()
    {
        //RollModifiers();
        CreateModifiedStats();

        //playerShooting = GetComponentInParent<PlayerShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        Reload();
        UpdateShotTimer();
    }

    public void CatchReferences()
    {
        Debug.Log("Catching Reference for weapon");
        PlayerData player = PlayerList.Instance.GetMyPlayer();
        playerData = player;
        gunManagement = playerData.gunManagement;

    }
    public void CatchReference(PlayerData owningPlayer)
    {
        playerData = owningPlayer;
        gunManagement = playerData.gunManagement;
    }

    public void CreateModifiedStats()
    {
        //Debug.Log("Creating Modified Stats");
        modifiedStats = new GunStats();
        modifiedStats.CopyDataFrom(baseStats);

        foreach(GunUpgradeBase upgrade in gunUpgrades)
        {
            upgrade.Apply(this);
        }
        //Debug.Log("Created Modified Stats for " + gameObject.name);
    }



    void CheckAmmo()
    {
        if (magazineCurrent <= 0)
        {
            reload = true;
        }
    }
    void Reload()
    {
        if(reload == true)
        {
            reloadTimer += Time.deltaTime;

            if(reloadTimer >= modifiedStats.reloadTime)
            {
                reloadTimer = 0;
                reload = false;

                int remainingAmount = magazineCurrent;
                magazineCurrent = 0;
                playerData.ammo.ModifyAmmo(gunType, remainingAmount);

                int possibleAmount = playerData.ammo.GetCurrentAmmo(gunType);

                if(possibleAmount < modifiedStats.magazineMax)
                {
                    playerData.ammo.ModifyAmmo(gunType, -possibleAmount);
                    magazineCurrent += possibleAmount;
                }
                else
                {
                    playerData.ammo.ModifyAmmo(gunType, -modifiedStats.magazineMax);
                    magazineCurrent += modifiedStats.magazineMax;
                }

                gunManagement.GunReloaded();
            }
        }
    }
    public void ForceReload()
    {
        reload = true;
        int remainingAmount = magazineCurrent;
        playerData.ammo.ModifyAmmo(gunType, remainingAmount);
        magazineCurrent = 0;
        playerData.ui.UpdateAmmo();
    }
    public void Shoot()
    {
        if (magazineCurrent <= 0)
        {
            return;
        }
        if(shotTimer > 0)
        {
            //Debug.Log("Timer PRoblem");
            return;
        }
        //Debug.Log("Shoooo");

        shotTimer += modifiedStats.timeBetweenShots;
        magazineCurrent--;
        CreateProjectile();
        CheckAmmo();
        
    }
    private void UpdateShotTimer()
    {

        shotTimer -= Time.deltaTime;
        if (shotTimer < 0)
        {
            shotTimer = 0;
        }
    }
    void CreateProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab.gameObject);
        projectile.transform.position = nozzle.transform.position;
        Vector3 direction = gunManagement.data.cameraHookUp.GetLookDirection();

        // Determine where player wants to shoot
        RaycastHit hit;
        Vector3 hitPoint;
        if (Physics.Raycast(gunManagement.data.cameraHookUp.cam.transform.position, direction, out hit, 200, bulletMask))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = gunManagement.data.cameraHookUp.forwardPos.position;
        }

        direction = (hitPoint - nozzle.transform.position).normalized;
        BulletData bulletData = projectile.GetComponent<BulletData>();
        //bulletData.projectileBehaviour.direction = direction * modifiedStats.projectileSpeed;
        projectile.transform.LookAt(hitPoint);
        FillBulletData(direction, bulletData);

        gunManagement.data.ShootBullet_ServerRPC(direction, presetID, gunManagement.data.OwnerClientId);
    }

    public void CreatePhantomProjectile(Vector3 dir)
    {
        //Debug.Log("Creating Phantom Projectile");
        //Debug.Log("Received Dir = " + dir);
        GameObject projectile = Instantiate(projectilePrefab.gameObject);
        projectile.transform.position = nozzle.transform.position;
        BulletData bulletData = projectile.GetComponent<BulletData>();
        //bulletData.projectileBehaviour.direction = dir;
        projectile.transform.LookAt(dir);
        FillBulletData(dir, bulletData);
        bulletData.projectileBehaviour.phantomBullet = true;
    }

    private void FillBulletData(Vector3 dir, BulletData newBullet)
    {
        //Debug.Log("Filling data with dir = " + dir);
        //newBullet.bulletInfo = projectilePrefab.bulletInfo;
        newBullet.projectileBehaviour.owningFaction = ENUM_Faction.player;
        newBullet.projectileBehaviour.direction = dir * modifiedStats.projectileSpeed;

        newBullet.bulletInfo.damageData = modifiedStats.damageArray;
        newBullet.bulletInfo.afflictionChance = modifiedStats.afflictionChance;
        newBullet.bulletInfo.critChance = modifiedStats.critChance;
        newBullet.bulletInfo.critDamageMultiplier = modifiedStats.critMultiplier;
        newBullet.bulletInfo.damageModifierWhenAfflicted = modifiedStats.damageMultipliersOnAffliction;
        newBullet.bulletInfo.punchThrough = modifiedStats.punchThrough;
    }

    public void GunDealtDamage(int val)
    {

    }
}
