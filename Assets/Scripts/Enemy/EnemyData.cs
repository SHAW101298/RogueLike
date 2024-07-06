using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : UnitData
{
    public CharacterController controller;
    public EnemyStats stats;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HitEnemy(GunDamageData damageData)
    {
        

    }
    public void HitEnemy(List<GunDamageData> dealtDamage)
    {
        foreach(GunDamageData damage in dealtDamage)
        {
            
        }
    }
}
