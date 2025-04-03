using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool isActive;
    public int roomTemplate;
    public List<EnemyData> enemiesInRoom;
    //[SerializeField] GameObject room;
    public RoomValidationScript roomValidationScript;
    public RoomGenerator floorParent;
    [Space(15)]
    public Transform entrance;
    public Transform exit;
    public Transform portal;
    //[SerializeField] GameObject placementColliders;
    [Header("Enemies Spawning")]
    [SerializeField] List<Room_SpawningData> spawningData;
    //public Transform interactablePosition;
    [Header("Randoms")]
    public List<Transform> breakablesPositions;
    public string breakablesLayOut;


    public bool Run_FOO;



    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Run_FOO == true)
        {
            Run_FOO = false;
        }
    }

    public void ActivateRoom()
    {
        gameObject.SetActive(true);
        //room.SetActive(true);

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
        gameObject.SetActive(false);
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
                data.ActivateAIForEnemies();
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

    public void GenerateBreakablesInRoom()
    {
        //Debug.LogWarning("Save this data in room, for use later");
        int randChance;
        int randBreakable;
        GameObject temp;
        BreakAbles tempBreakAble;
        if(roomValidationScript.id < 10)
        {
            breakablesLayOut += "0";
        }
        breakablesLayOut += roomValidationScript.id;

        // Loop for Spots
        for (int i = 0; i < breakablesPositions.Count; i++)
        {
            randChance = UnityEngine.Random.Range(0, 100);


            // Generate Breakables at specified Positions
            if (randChance <= floorParent.breakableSpawnChance)
            {
                randBreakable = UnityEngine.Random.Range(0, floorParent.breakables.Count);
                tempBreakAble = floorParent.breakables[randBreakable];
                temp = Instantiate(tempBreakAble.gameObject);
                temp.transform.SetParent(breakablesPositions[i]);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localEulerAngles = Vector3.zero;

                breakablesLayOut += tempBreakAble.GetTemplateID();
            }
            else
            {
                breakablesLayOut += "00";
            }
        }

        /* Destroy Empty Spots
        for (int i = room.breakablesPositions.Count - 1; i > 0; i--)
        {
            if (room.breakablesPositions[i].childCount == 0)
            {
                Destroy(room.breakablesPositions[i].gameObject);
            }
        }
        room.breakablesPositions.TrimExcess();
        */
    }

    public void GenerateBreakablesInRoomFromLayout()
    {
        string temp = "" + breakablesLayOut[0] + breakablesLayOut[1];
        int breakAbleTemplate = 0;
        int x = breakablesLayOut.Length;
        GameObject tempGO;

        int room = Convert.ToInt32(temp);
        //Debug.Log("Validation is = " + roomValidationScript.id + "| Room is = " + room);
        temp = "";
        for(int i = 2; i < x; i+= 2)
        {
            //Debug.Log("i = " + i);
            temp += "" + breakablesLayOut[i] + breakablesLayOut[i+1];
            //Debug.Log("temp = " + temp);
            breakAbleTemplate = Convert.ToInt32(temp);
            //Debug.Log("breakAbleTemplate =  " + breakAbleTemplate);
            if(breakAbleTemplate != 0)
            {
                tempGO = Instantiate(floorParent.breakables[breakAbleTemplate-1].gameObject);
                tempGO.transform.SetParent(breakablesPositions[(i / 2)-1].transform);
                tempGO.transform.localPosition = Vector3.zero;
            }
            temp = "";
        }
    }

}
