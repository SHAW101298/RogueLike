using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_EnterTrigger : MonoBehaviour
{
    [SerializeField] RoomManager room;
    [SerializeField] LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(Tools.CheckIfInMask(playerLayer, other.gameObject.layer) == true)
        {
            room.EVENT_PlayerEnteringRoom();
        }
    }
}
