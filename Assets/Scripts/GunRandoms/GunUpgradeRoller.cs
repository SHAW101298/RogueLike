using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgradeRoller : MonoBehaviour
{
    public static GunUpgradeRoller ins;
    private void Awake()
    {
        ins = this;
    }

    public GunUpgradeBase GetRandomRoll()
    {
        int maxInt = (int)ENUM_GunRandomUpgradeType.reloadSpeed;
        int randomInt = Random.Range(0, 1 + maxInt);

        ENUM_GunRandomUpgradeType upgradeType = (ENUM_GunRandomUpgradeType)randomInt;
        GunUpgradeBase rolledUpgrade = new GunUpgradeBase();

        switch(upgradeType)
        {
            case ENUM_GunRandomUpgradeType.ammoMax:
                rolledUpgrade = rollAmmoMax();
                break;
            case ENUM_GunRandomUpgradeType.magazineSize:
                rolledUpgrade = rollMagazineSize();
                break;
            case ENUM_GunRandomUpgradeType.damage:
                rolledUpgrade = rollDamage();
                break;
            case ENUM_GunRandomUpgradeType.punchThrough:
                rolledUpgrade = rollPunchThrough();
                break;
            case ENUM_GunRandomUpgradeType.reloadSpeed:
                rolledUpgrade = rollReloadSpeed();
                break;
        }

        return rolledUpgrade;
    }

    GunUpgradeAmmoMax rollAmmoMax()
    {
        GunUpgradeAmmoMax upgrade = new GunUpgradeAmmoMax();
        upgrade.Roll();
        return upgrade;
    }
    GunUpgradeMagazineMax rollMagazineSize()
    {
        GunUpgradeMagazineMax upgrade = new GunUpgradeMagazineMax();
        upgrade.Roll();
        return upgrade;
    }
    GunUpgradeDamage rollDamage()
    {
        GunUpgradeDamage upgrade = new GunUpgradeDamage();
        upgrade.Roll();
        return upgrade;
    }
    GunUpgradeReloadSpeed rollReloadSpeed()
    {
        GunUpgradeReloadSpeed upgrade = new GunUpgradeReloadSpeed();
        upgrade.Roll();
        return upgrade;
    }
    GunUpgradePunchThrough rollPunchThrough()
    {
        GunUpgradePunchThrough upgrade = new GunUpgradePunchThrough();
        upgrade.Roll();
        return upgrade;
    }
}
