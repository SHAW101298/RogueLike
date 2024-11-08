using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoomGenerator : NetworkBehaviour
{
    #region
    public static RoomGenerator Instance;
    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public RoomManager spawnArea;
    //public Transform spawnAreaExit;
    public List<GameObject> possibleRooms;
    public List<RoomManager> currentRooms;
    public int roomsToGenerate;

    [Header("Debug")]
    public bool generateRooms;

    private void Update()
    {
        if(generateRooms == true)
        {
            foreach(RoomManager room in currentRooms)
            {
                Destroy(room.gameObject);
            }
            currentRooms.Clear();
            generateRooms = false;
            GenerateRooms();
        }
    }
    public void GenerateRooms()
    {
        Debug.Log("Generating Rooms");
        if (IsOwner == false)
        {
            Debug.LogWarning("Not Owner");
            return;
        }
        currentRooms.Add(spawnArea);
        Vector3 nextSpot = spawnArea.exit.position;
        GameObject newRoom;
        RoomManager createdRoom;

        for(int i = 1; i < roomsToGenerate; i++)
        {
            newRoom = Instantiate(GetRandomRoom());
            createdRoom = newRoom.GetComponent<RoomManager>();
            createdRoom.roomValidationScript.id = i;
            newRoom.transform.position = nextSpot;
            nextSpot = createdRoom.exit.position;
        }
    }
    void GenerateRooms(int startingId)
    {
        Debug.Log("Generating Rooms after Error");

        Vector3 nextSpot = currentRooms[startingId].exit.position;
        GameObject newRoom;
        RoomManager createdRoom;

        for (int i = startingId +1; i < roomsToGenerate; i++)
        {
            newRoom = Instantiate(GetRandomRoom());
            createdRoom = newRoom.GetComponent<RoomManager>();
            createdRoom.roomValidationScript.id = i;
            newRoom.transform.position = nextSpot;
            nextSpot = createdRoom.exit.position;
        }
    }

    GameObject GetRandomRoom()
    {
        int val = Random.Range(0, possibleRooms.Count);
        return possibleRooms[val];
    }
    public void Alert_RoomCollide(int id)
    {
        for(int i = currentRooms.Count-1; i >= id; i--)
        {
            Destroy(currentRooms[i].gameObject);
            currentRooms.RemoveAt(i);
        }
        GenerateRooms(id);
    }
}
