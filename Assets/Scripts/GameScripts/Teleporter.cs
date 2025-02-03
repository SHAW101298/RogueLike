using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractableBase
{
    public Transform teleportPosition;
    public RoomManager interactedRoom;
    public RoomManager currentRoom;

    public RoomManager destinationRoom;
    public RoomGenerator destinationFloor;
    public RoomGenerator currentFloor;

    public override void Interact(PlayerData data)
    {
        // Activate Room
        // Teleport Player
        Teleport(data);

        //interactedRoom.ActivateRoom();
        //interactedRoom.ActivateRoom();
        //data.TeleportPlayer(teleportPosition.position);
        //currentRoom.DeactivateRoomForMe();
    }

    void Teleport(PlayerData data)
    {
        if(destinationRoom != null)
        {
            data.TeleportPlayer(destinationRoom.entrance.position);
        }
        else
        {
            Debug.Log("Teleporting to floor  " + destinationFloor.spawnArea.name);
            data.TeleportPlayer(destinationFloor.spawnArea.entrance.position);
            destinationFloor.ActivateFloorForMe();

            // Spawn Case
            if (currentFloor != null)
            {
                Debug.Log("Deactivating  " + currentFloor.spawnArea.name);
                currentFloor.DeactivateFloorForMe();
            }
            else
            {
                Debug.Log("Current floor is " + currentFloor.spawnArea.name);
            }
        }

    }
}
