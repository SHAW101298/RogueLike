using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
            //Debug.Log("Target not in mask. Tag is " + other.gameObject.tag);
            return;
        }

        //Debug.Log("Target in mask");
        switch (other.gameObject.tag)
        {
            case "Player":
                CollisionPlayer(other);
                break;
            case "Enemy":
                CollisionEnemy(other);
                break;
            case "EnemyWeakSpot":
                CollisionEnemyWeakSpot(other);
                break;
            case "HardSurface":
                CollisionHardSurface(other);
                break;
            case "BreakAble":
                CollisionBreakAble(other);
                break;
            default:
                //Debug.Log("Unresolved TAG  |  " + other.gameObject.tag);
                break;
        }
    }

    void CollisionPlayer(Collider other)
    {
        //Debug.Log("Collistion Player");
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
        if(owningFaction == ENUM_Faction.enemy)
        {
            //Debug.Log("Player Hit");
            PlayerData player = other.gameObject.GetComponent<PlayerData>();
            if(PlayerList.Instance.GetMyPlayer() == player)
            {
                player.HitPlayer(data.bulletInfo);
            }




            data.bulletInfo.punchThrough--;
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }

    }
    void CollisionEnemy(Collider other)
    {
        //Debug.Log("Collistion Enemy");
        if (phantomBullet == true)
        {
            data.bulletInfo.punchThrough--;
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Bullet Shot by Enemy hit enemy
        if (owningFaction != ENUM_Faction.player)
        {
            return;
        }
        if (data.bulletInfo.punchThrough <= 0)
        {
            return;
        }
        data.bulletInfo.punchThrough--;

        EnemyData enemyData = other.gameObject.GetComponentInParent<EnemyData>();
        HitInfo_Player hitInfo = data.owningGun.playerData.hitInfo;
        hitInfo.SetData(data.owningGun, enemyData, false);
        hitInfo.Calculate();
        DamageInfo damageInfo = hitInfo.damageInfo;
        float calculatedDamage = damageInfo.GetDamageAmount();
        HitResult result = enemyData.HitEnemy(damageInfo);
        result.TriggerEvents();


        if (data.bulletInfo.punchThrough <= 0)
        {
            //Debug.Log("Destroying Object name = " + gameObject.name);
            Destroy(gameObject);
        }
    }
    void CollisionEnemyWeakSpot(Collider other)
    {
        //Debug.Log("Collision Weak Spot");
        if (phantomBullet == true)
        {
            data.bulletInfo.punchThrough--;
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Bullet Shot by Enemy hit enemy
        if (owningFaction != ENUM_Faction.player)
        {
            return;
        }
        if(data.bulletInfo.punchThrough <= 0)
        {
            return;
        }
        data.bulletInfo.punchThrough--;

        // Calculate damage Dealt
        EnemyData enemyData = other.gameObject.GetComponentInParent<EnemyData>();
        HitInfo_Player hitInfo = data.owningGun.playerData.hitInfo;
        hitInfo.SetData(data.owningGun, enemyData, true);
        hitInfo.Calculate();
        DamageInfo damageInfo = hitInfo.damageInfo;
        float calculatedDamage = damageInfo.GetDamageAmount();
        HitResult result = enemyData.HitEnemy(damageInfo);
        result.TriggerEvents();

        // Destroy bullet if it hit its limit
        if (data.bulletInfo.punchThrough <= 0)
        {
            Destroy(gameObject);
        }
    }
    void CollisionHardSurface(Collider other)
    {
        //Debug.Log("Collistion Hard Surface");
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
        Debug.Log("Collisiton BreakAble");
        if (phantomBullet == true)
        {
            if (data.bulletInfo.punchThrough <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }
        BreakAbles breakable = other.gameObject.GetComponent<BreakAbles>();
        float damage = 0;
        foreach(DamageData dmg in data.bulletInfo.damageData)
        {
            damage += dmg.damage;
        }
        breakable.TakeDamage(damage);
        data.bulletInfo.punchThrough--;

        if (data.bulletInfo.punchThrough <= 0)
        {
            Destroy(gameObject);
        }

    }
}
