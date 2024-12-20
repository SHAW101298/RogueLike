﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletInfo
{
    public List<DamageData> damageData;
    public ElementalTable damageModifierWhenAfflicted;
    public float critChance = 0;
    public float critDamageMultiplier = 1;
    public float afflictionChance = 0;
    public int punchThrough = 1;
}

public class ProjectileBehaviour : MonoBehaviour
{
    public bool phantomBullet;
    public BulletData data;
    public LayerMask collideMask;
    public ENUM_Faction owningFaction;
    public Rigidbody rb;
    public Vector3 direction;
    private void FixedUpdate()
    {
        //rb.AddForce(direction, ForceMode.Force);
        rb.velocity = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Tools.CheckIfInMask(collideMask, other.gameObject.layer) == false)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Player":
                CollisionPlayer(other);
                break;
            case "Enemy":
                CollisionEnemy(other);
                break;
            case "HardSurface":
                CollisionHardSurface(other);
                break;
            case "BreakAble":
                CollisionBreakAble(other);
                break;
            default:
                break;
        }
    }


    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision with = " + collision.collider.gameObject);
        switch (collision.collider.gameObject.tag)
        {
            case "Player":
                CollisionPlayer(collision);
                break;
            case "Enemy":
                CollisionEnemy(collision);
                break;
            case "HardSurface":
                CollisionHardSurface(collision);
                break;
            case "BreakAble":
                CollisionBreakAble(collision);
                break;
            default:
                break;
        }
        
        
    }
    */

    void CollisionPlayer(Collider other)
    {
        if (phantomBullet == true)
        {
            data.bulletInfo.punchThrough--;
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Bullet shot by Player hit another Player
        if (owningFaction == ENUM_Faction.player)
        {
            return;
        }
        Debug.Log("Player Hit");

    }
    void CollisionEnemy(Collider other)
    {
        if(phantomBullet == true)
        {
            data.bulletInfo.punchThrough--;
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Bullet Shot by Enemy hit enemy
        if(owningFaction != ENUM_Faction.player)
        {
            return;
        }
        data.bulletInfo.punchThrough--;

        EnemyData enemyData = other.gameObject.GetComponent<EnemyData>();

        float dealtDamage = enemyData.HitEnemy(data.bulletInfo);

        if (data.bulletInfo.punchThrough <= 0)
        {
            Destroy(gameObject);
        }
    }
    void CollisionHardSurface(Collider other)
    {
        if(phantomBullet == true)
        {
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }

        Destroy(gameObject);
    }
    void CollisionBreakAble(Collider other)
    {
        if (phantomBullet == true)
        {
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }

        Debug.Log("Probably Some HP Implementation");
        Destroy(other.gameObject);
        data.bulletInfo.punchThrough--;

        if (data.bulletInfo.punchThrough <= 0)
        {
            Destroy(gameObject);
        }

    }
}
