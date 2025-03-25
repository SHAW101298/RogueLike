using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_EventSystem : MonoBehaviour
{
    public UnityEvent playerShotBullet;
    public BulletData lastShotBullet;
    public HitInfo_Player hitInfoPlayer;


    public UnityEvent OnEnemyWeaponHitEvent;
    public UnityEvent OnEnemyAbilityHitEvent;
    public UnityEvent OnEnemyBlessingHitEvent;

    public UnityEvent OnCriticalHitEvent;
    public UnityEvent OnNonCriticalHitEvent;
    public UnityEvent OnAfflictionAppliedEvent;

    public UnityEvent OnReloadEmptyEvent;
    public UnityEvent OnReloadHalfEmptyEvent;
    public UnityEvent OnReloadEvent;

    public UnityEvent OnHealthPickUpEvent;
    public UnityEvent OnAmmoPickUpEvent;
    public UnityEvent OnGoldPickUpEvent;
    public UnityEvent OnBlessingPickUpEvent;
    public UnityEvent OnGunPickUpEvent;

    public UnityEvent OnTakeHealthDamageEvent;
    public UnityEvent OnTakeShieldDamageEvent;
    public UnityEvent OnShieldDepleted;
    public UnityEvent OnShieldRegenerationStart;

    public UnityEvent OnEnemyKillEvent;
    public UnityEvent OnDeathEvent;
    public UnityEvent OnBossKillEvent;
    public UnityEvent OnChallengeRoomCompletedEvent;

    public UnityEvent OnDashEvent;
    public UnityEvent OnItemBoughtEvent;
    public UnityEvent OnAbilityUseEvent;
    public UnityEvent OnJumpEvent;
    public UnityEvent OnShootEvent;
    public UnityEvent OnWeaponSwapEvent;

    public UnityEvent OnFirstHit;
    public UnityEvent OnHalfHealthTap;
    public UnityEvent OnWeakPointHit;

    //Every X seconds
    //Every X meters moved
    //Every X jump
    //Every X shot

    private void Start()
    {
    }
}
