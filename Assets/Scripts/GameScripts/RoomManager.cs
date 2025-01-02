using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoomManager : NetworkBehaviour
{
    public bool isActive;
    public int roomTemplate;
    public List<EnemyData> enemiesInRoom;
    [SerializeField] GameObject room;
    public RoomValidationScript roomValidationScript;
    [Space(15)]
    public Transform entrance;
    public Transform exit;
    public Transform portal;
    [SerializeField] GameObject placementColliders;



    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateRoom()
    {
        room.SetActive(true);

        // Room already active, nothing to do
        if (isActive == true)
            return;

        // Called by client to open a room
        if(IsOwner == false)
        {
            Debug.Log("Im not owner, ask server to activate room");
            // Make RPC call to host to open room
            ActivateThisRoom_ServerRPC();
        }
        else
        {
            // Ask all clients to open this room
            ActivateThisRoom_ClientRPC();
        }
    }
    public void DeactivateRoomForMe()
    {
        room.SetActive(false);
    }
    public void EVENT_PlayerEnteringPortal()
    {
        // Check if room is already active
        ActivateRoom();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ActivateThisRoom_ServerRPC()
    {
        Debug.Log("Server RPC Activate This Room");
        // Ask each client to activate this room
        ActivateThisRoom_ClientRPC();
    }

    [ClientRpc]
    public void ActivateThisRoom_ClientRPC()
    {
        Debug.Log("Called on client ActivateThisRoom");
        // Server tells to activate this room
        room.SetActive(true);
        isActive = true;


        // Make enemies Seek players out
        foreach(EnemyData enemy in enemiesInRoom)
        {
            enemy.ActivateEnemy();
        }

    }

}
