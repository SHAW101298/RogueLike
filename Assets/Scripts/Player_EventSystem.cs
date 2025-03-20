using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_EventSystem : MonoBehaviour
{
    public UnityEvent playerShotBullet;
    public BulletData lastShotBullet;


    public UnityEvent OnEnemyWeaponHit;
    public UnityEvent OnEnemyAbilityHit;
    public UnityEvent OnEnemyBlessingHit;

    public UnityEvent OnCriticalHit;
    public UnityEvent OnNonCriticalHit;
    public UnityEvent OnAfflictionApplied;

    public UnityEvent OnReloadEmpty;
    public UnityEvent OnReloadHalfEmpty;
    public UnityEvent OnReload;

    public UnityEvent OnHealthPickUp;
    public UnityEvent OnAmmoPickUp;
    public UnityEvent OnGoldPickUp;
    public UnityEvent OnBlessingPickUp;
    public UnityEvent OnGunPickUp;

    public UnityEvent OnTakeHealthDamage;
    public UnityEvent OnTakeShieldDamage;

    public UnityEvent OnEnemyKill;
    public UnityEvent OnDeath;
    public UnityEvent OnBossKill;
    public UnityEvent OnChallengeRoomCompleted;

    public UnityEvent OnDash;
    public UnityEvent OnItemBought;
    public UnityEvent OnAbilityUse;
    public UnityEvent OnJump;
    public UnityEvent OnShoot;
    public UnityEvent OnWeaponSwap;

//Every X seconds
//Every X meters moved
//Every X jump
//Every X shot

}
