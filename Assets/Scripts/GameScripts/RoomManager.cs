using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool isActive;
    public int roomTemplate;
    public List<EnemyData> enemiesInRoom;
    [SerializeField] GameObject room;
    public RoomValidationScript roomValidationScript;
    public RoomGenerator floorParent;
    [Space(15)]
    public Transform entrance;
    public Transform exit;
    public Transform portal;
    [SerializeField] GameObject placementColliders;
    [Header("Enemies Spawning")]
    [SerializeField] List<Room_SpawningData> spawningData;



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
        if(NetworkManager.Singleton.IsHost == false)
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
    public void EVENT_PlayerEnteringRoom()
    {
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
        isActive = true;

        SpawnEnemies();

        // Make enemies Seek players out
        foreach(EnemyData enemy in enemiesInRoom)
        {
            enemy.ActivateEnemy();
            GameOptions.Instance.ApplyDifficultySettings(enemy);
        }

    }

    public void SpawnEnemies()
    {
        //Debug.Log("Spawning Enemies");
        if (spawningData.Count > 0)
        {
            //Debug.Log("Instances to create = " + spawningData.Count);
            foreach(Room_SpawningData data in spawningData)
            {
                data.SpawnUnits();
            }
        }
    }
    public void DestroyEnemiesInRoom()
    {
        for(int i = enemiesInRoom.Count -1; i >= 0; i--)
        {
            Destroy(enemiesInRoom[i].gameObject);
        }
        enemiesInRoom.Clear();
    }

}
