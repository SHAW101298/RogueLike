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
    public GameObject projectilePrefab;
    public GameObject nozzle;
    public PlayerShooting playerShooting;
    public PlayerData playerData;

    [Header("Debug")]
    public bool ForceGun1 = true;

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

    public void RollModifiers()
    {
        GunUpgradeBase roll = GunUpgradeRoller.ins.GetRandomRoll();
        gunUpgrades.Add(roll);
        roll = GunUpgradeRoller.ins.GetRandomRoll();
        gunUpgrades.Add(roll);

    }
    public void CreateModifiedStats()
    {
        modifiedStats = baseStats;

        for(int i = 0; i < gunUpgrades.Count; i++)
        {
            GunUpgradeBase currentUpgrade = gunUpgrades[i];
            //Debug.Log("Upgrade Type = " + currentUpgrade.upgradeType);
            switch(currentUpgrade.upgradeType)
            {
                case ENUM_GunRandomUpgradeType.ammoMax:
                    UpgradeAmmoMax(currentUpgrade);
                    break;
                case ENUM_GunRandomUpgradeType.magazineSize:
                    UpgradeMagazineMax(currentUpgrade);
                    break;
                case ENUM_GunRandomUpgradeType.damage:
                    UpgradeDamage(currentUpgrade);
                    break;
                case ENUM_GunRandomUpgradeType.punchThrough:
                    UpgradePunchThrough(currentUpgrade);
                    break;
                case ENUM_GunRandomUpgradeType.reloadSpeed:
                    UpgradeReloadSpeed(currentUpgrade);
                    break;
                default:
                    Debug.LogError("Unspecified Upgrade Type ! =  " + currentUpgrade.upgradeType);
                    break;
            }
        }
        //Debug.Log("Created Modified Stats for " + gameObject.name);
    }

    private void UpgradeAmmoMax(GunUpgradeBase currentUpgrade)
    {
        Debug.Log(currentUpgrade.GetType());
        GunUpgradeAmmoMax ammoUpgrade = (GunUpgradeAmmoMax)currentUpgrade;
        if(ammoUpgrade.flatAmount != 0)
        {
            modifiedStats.ammoMax += ammoUpgrade.flatAmount;
        }
        else
        {
            modifiedStats.ammoMax += (int)(baseStats.ammoMax * (ammoUpgrade.percentageAmount)) - baseStats.ammoMax;
        }
    }
    private void UpgradeMagazineMax(GunUpgradeBase currentUpgrade)
    {
        GunUpgradeMagazineMax magazineMax = (GunUpgradeMagazineMax)currentUpgrade;
        if (magazineMax.flatAmount != 0)
        {
            modifiedStats.magazineMax += magazineMax.flatAmount;
        }
        else
        {
            modifiedStats.magazineMax += (int)(baseStats.magazineMax * (magazineMax.percentageAmount));
        }
    }
    private void UpgradeDamage(GunUpgradeBase currentUpgrade)
    {
        GunUpgradeDamage damage = (GunUpgradeDamage)currentUpgrade;

        GunDamageData newdamage = new GunDamageData();
        newdamage.damageType = damage.damageType;
        //Debug.Log(damage.ToString());
        
        modifiedStats.damageArray.Add(newdamage);
        if(damage.flatAmount != 0)
        {
            newdamage.damage = damage.flatAmount;
        }
        else
        {
            newdamage.damage = (int)(baseStats.damageArray[0].damage * damage.percentageAmount);
        }
    }
    private void UpgradeReloadSpeed(GunUpgradeBase currentUpgrade)
    {
        GunUpgradeReloadSpeed reloadSpeed = (GunUpgradeReloadSpeed)currentUpgrade;
        if (reloadSpeed.flatAmount != 0)
        {
            modifiedStats.reloadTime -= reloadSpeed.flatAmount;
            if(modifiedStats.reloadTime < 0)
            {
                modifiedStats.reloadTime = 0;
            }    
        }
        else
        {
            modifiedStats.reloadTime -= baseStats.reloadTime * reloadSpeed.percentageAmount;
            if (modifiedStats.reloadTime < 0)
            {
                modifiedStats.reloadTime = 0;
            }
        }
    }
    private void UpgradePunchThrough(GunUpgradeBase currentUpgrade)
    {
        GunUpgradePunchThrough punch = (GunUpgradePunchThrough)currentUpgrade;

        modifiedStats.punchThrough += punch.flatAmount;
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
        GameObject temp = Instantiate(projectilePrefab);
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
        FillBulletData(bulletData);
    }
    void FillBulletData(BulletData bulletData)
    {
        bulletData.projectileBehaviour.owningGun = this;
        bulletData.projectileBehaviour.owningPlayer = playerData;
        bulletData.projectileBehaviour.punchThrough = modifiedStats.punchThrough;
    }
    public void RefillAmmoToMax()
    {
        ammoCurrent = modifiedStats.ammoMax;
        magazineCurrent = modifiedStats.magazineMax;
    }
}
