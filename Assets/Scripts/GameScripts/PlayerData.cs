using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
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
    public void TeleportPlayer(Vector3 pos)
    {
        transform.position = pos;
    }
}
