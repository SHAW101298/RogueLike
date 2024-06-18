using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;


    public void DecreaseHealth(GunDamageData damageData)
    {
        Debug.Log("Decreasing Health by = " + damageData.damage + "   |   TYPE = " + damageData.damageType);
        health -= damageData.damage;

        if(health <= 0)
        {
            Debug.Log("Unit should be dead, but we will save it for now");
        }
    }


}
