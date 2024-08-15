using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTINGSCRIPT : MonoBehaviour
{
    public EnemyData enemy;
    public Gun gun;

    [Header("Debug")]
    public bool runFunction;
    public bool runCOO;
    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        if(runFunction == true)
        {
            runFunction = false;
            BOO();
        }
        if(runCOO == true)
        {
            runCOO = false;
            COO();
        }
    }
    void BOO()
    {
        Debug.LogWarning("BOO");
        enemy.HitEnemy(gun);
    }
    void COO()
    {
        Debug.LogWarning("COO");
        GunUpgradeBase upgrade = GunUpgradeRoller.ins.GetRandomRoll();
        gun.gunUpgrades.Add(upgrade);
        upgrade = GunUpgradeRoller.ins.GetRandomRoll();
        gun.gunUpgrades.Add(upgrade);
        gun.CreateModifiedStats();
        //RESIZETEST.ins.ShowData(gun);
        UI_RaycastedGunData.Instance.ShowGunData(gun);
    }
}
