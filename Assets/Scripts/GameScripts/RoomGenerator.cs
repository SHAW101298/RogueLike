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

    public int earliestError = -1;
    public bool regenerateRooms;

    [Header("Debug")]
    public bool generateRooms;
    public int iteration = 0;
    public int maxIteration;

    private void Update()
    {
        if(generateRooms == true)
        {
            for(int i = 1; i < currentRooms.Count;i++)
            {
                Destroy(currentRooms[i].gameObject);
            }
            /*
            foreach(RoomManager room in currentRooms)
            {
                Destroy(room.gameObject);
            }
            */
            currentRooms.Clear();
            generateRooms = false;
            GenerateRooms();
        }
        if(regenerateRooms == true)
        {
            if(iteration > maxIteration )
            {
                Debug.Log("Iteration Breached");
                return;
            }
            regenerateRooms = false;
            RegenerateRooms();
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
            currentRooms.Add(createdRoom);
        }
    }
    void GenerateRooms(int startingId)
    {
        //Debug.Log("Generating Rooms after Error. ID = " + startingId);

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
            currentRooms.Add(createdRoom);
        }
        iteration++;
    }

    GameObject GetRandomRoom()
    {
        int val = Random.Range(0, possibleRooms.Count);
        Debug.Log("Creating Room " + possibleRooms[val].gameObject.name);
        return possibleRooms[val];
    }
    public void Alert_RoomCollide(int id)
    {
        Debug.Log("Error on ID " + id);
        //Debug.Break();

        if(earliestError == -1)
        {
            earliestError = id;
            regenerateRooms = true;
        }
        if(earliestError != -1 && earliestError > id)
        {
            earliestError = id;
            regenerateRooms = true;
        }

        /*
        for(int i = currentRooms.Count-1; i > id; i--)
        {
            Destroy(currentRooms[i].gameObject);
            currentRooms.RemoveAt(i);
        }
        GenerateRooms(id);
        */
    }
    void RegenerateRooms()
    {
        Debug.Log("============================\n\n");
        Debug.Log("Destroying Rooms from " + earliestError);
        for(int i = currentRooms.Count-1; i > earliestError; i--)
        {
            Destroy(currentRooms[i].gameObject);
            currentRooms.RemoveAt(i);
        }
        GenerateRooms(earliestError);
        earliestError = -1;
    }
}
