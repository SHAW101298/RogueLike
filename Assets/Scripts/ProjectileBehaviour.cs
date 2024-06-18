using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public PlayerData owningPlayer;
    public Vector3 direction;
    public Rigidbody rb;

    public Gun owningGun;
    public int punchThrough;
    private void FixedUpdate()
    {
        //rb.AddForce(direction, ForceMode.Force);
        rb.velocity = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
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

    }
    void CollisionEnemy(Collider other)
    {
        punchThrough--;
        EnemyData enemyData = other.gameObject.GetComponent<EnemyData>();
        GunStats gunStats = owningGun.modifiedStats;
        for(int i = 0; i < gunStats.damageArray.Count; i++)
        {
            enemyData.stats.DecreaseHealth(gunStats.damageArray[i]);
        }

        if(punchThrough <= 0)
        {
            Destroy(gameObject);
        }

    }
    void CollisionHardSurface(Collider other)
    {
        Destroy(gameObject);
    }
    void CollisionBreakAble(Collider other)
    {
        Debug.Log("Probably Some HP Implementation");
        Destroy(other.gameObject);
        punchThrough--;

        if (punchThrough <= 0)
        {
            Destroy(gameObject);
        }

    }
}
