using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : InteractableBase
{
    [SerializeField] RoomManager room;

    public override void Interact(PlayerData data)
    {
        data.TeleportPlayer(room.GetSpawnPosition());
        room.EVENT_PlayerEnteringPortal();
        // Activate Room

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            // Player Entered
            room.EVENT_PlayerEnteringPortal();
        }
    }


}
