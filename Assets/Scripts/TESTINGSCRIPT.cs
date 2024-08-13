using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTINGSCRIPT : MonoBehaviour
{
    public EnemyData enemy;
    public Gun gun;

    [Header("Debug")]
    public bool runFunction;
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
    }
    void BOO()
    {
        Debug.LogWarning("BOO");
        enemy.HitEnemy(gun);
    }
}
