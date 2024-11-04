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
        Debug.Log("player name = " + data.name);
        Debug.Log("Pos = " + data.gameObject.transform.position);
        data.TeleportPlayer(teleportPosition.position);
        Debug.Log("Pos = " + data.gameObject.transform.position);
        currentRoom.DeactivateRoomForMe();
    }
}
