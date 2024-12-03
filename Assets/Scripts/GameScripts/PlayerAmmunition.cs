using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmunition : MonoBehaviour
{
    public PlayerData data;

    public int SMG;
    public int SMGMax;
    public int ShotGun;
    public int ShotGunMax;
    public int Sniper;
    public int SniperMax;
    public int RocketLauncher;
    public int RocketLauncherMax;

    public int GetCurrentAmmo(ENUM_GunType type)
    {
        int ammo;
        switch(type)
        {
            case ENUM_GunType.pistol:
                ammo = 100;
                break;
            case ENUM_GunType.smg:
                ammo = SMG;
                break;
            case ENUM_GunType.shotgun:
                ammo = ShotGun;
                break;
            case ENUM_GunType.sniper:
                ammo = Sniper;
                break;
            case ENUM_GunType.rocketLauncher:
                ammo = RocketLauncher;
                break;
            default:
                ammo = 0;
                break;
        }
        return ammo;
    }

    public void ModifyAmmo(ENUM_GunType type, int value)
    {
        switch (type)
        {
            case ENUM_GunType.pistol:
                // Pistol is Unlimited
                break;
            case ENUM_GunType.smg:
                SMG += value;
                break;
            case ENUM_GunType.shotgun:
                ShotGun += value;
                break;
            case ENUM_GunType.sniper:
                Sniper += value;
                break;
            case ENUM_GunType.rocketLauncher:
                RocketLauncher += value;
                break;
            default:
                Debug.LogError("Invalid Gun TYPE");
                break;
        }
    }
}
