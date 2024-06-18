using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData : UnitData
{
    public PlayerStats stats;
    public PlayerMovement movement;
    public PlayerRotation rotation;
    public PlayerShooting shooting;
    public PlayerUI ui;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool AttemptGunChange(Gun gun)
    {
        bool success = shooting.AttemptGunChange(gun);
        return success;
    }

}
