using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    [Header("Data")]
    public GunStats baseStats;
    [Space(10)]
    public GunStats modifiedStats;

    public List<GunUpgradeBase> gunUpgrades;
    [Space(25)]
    public int magazineCurrent;
    public int ammoCurrent;
    float reloadTimer;
    float shotTimer;
    bool reload;
    public LayerMask bulletMask;
    public bool canBeSwapped; // Use During Picking Up New Weapons

    [Header("Ref")]
    public BulletData projectilePrefab;
    public GameObject nozzle;
    public PlayerShooting playerShooting;
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
        if(ForceGun1 == true)
        {
            playerShooting.currentlySelected = playerShooting.gun1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Reload();
        UpdateShotTimer();
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

                ammoCurrent += magazineCurrent;
                magazineCurrent = 0;

                if(ammoCurrent > modifiedStats.magazineMax)
                {
                    magazineCurrent = modifiedStats.magazineMax;
                }
                else
                {
                    magazineCurrent = ammoCurrent;
                }
                ammoCurrent -= magazineCurrent;
                playerShooting.GunReloaded();
            }
        }
    }
    public void Shoot()
    {
        if (magazineCurrent <= 0)
        {
            return;
        }
        if(shotTimer > 0)
        {
            return;
        }

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
        GameObject temp = Instantiate(projectilePrefab.gameObject);
        temp.transform.position = nozzle.transform.position;
        Vector3 direction = playerShooting.crossHairPos.position - playerShooting.pointOfView.transform.position;

        RaycastHit hit;
        Vector3 hitPoint;
        if (Physics.Raycast(playerShooting.pointOfView.transform.position, direction, out hit, 200, bulletMask))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = playerShooting.crossHairPos.transform.position;
        }

        direction = (hitPoint - nozzle.transform.position).normalized;
        BulletData bulletData = temp.GetComponent<BulletData>();
        bulletData.projectileBehaviour.direction = direction * modifiedStats.projectileSpeed;
        temp.transform.LookAt(hitPoint);
        FillBulletData(direction, bulletData);
    }

    private void FillBulletData(Vector3 dir, BulletData newBullet)
    {
        newBullet.bulletInfo = projectilePrefab.bulletInfo;
        newBullet.projectileBehaviour.owningFaction = ENUM_Faction.player;
        newBullet.projectileBehaviour.direction = dir * modifiedStats.projectileSpeed;
    }
    public void RefillAmmoToMax()
    {
        ammoCurrent = modifiedStats.ammoMax;
        magazineCurrent = modifiedStats.magazineMax;
    }
    public void GunDealtDamage(int val)
    {

    }
}
