using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractableBase
{
    public Transform teleportPosition;
    public RoomManager interactedRoom;
    public RoomManager currentRoom;

    public override void Interact(PlayerData data)
    {
        // Activate Room
        // Teleport Player

        interactedRoom.ActivateRoom(data);
        //interactedRoom.ActivateRoom();
        data.TeleportPlayer(teleportPosition.position);
        currentRoom.DeactivateRoomForMe();
    }
}
