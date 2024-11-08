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
    public bool clearRooms;
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
        if(clearRooms == true)
        {
            clearRooms = false;
            for(int i = currentRooms.Count-1; i > 0; i--)
            {
                Destroy(currentRooms[i].gameObject);
                currentRooms.RemoveAt(i);
            }
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
        Vector3 nextRotation = spawnArea.exit.eulerAngles;
        GameObject newRoom;
        RoomManager createdRoom;
        int lastRoomTemplate = currentRooms[0].roomTemplate;

        for (int i = 1; i < roomsToGenerate; i++)
        {
            int newRoomTemplate = GetRandomRoomExcept(lastRoomTemplate);
            newRoom = Instantiate(possibleRooms[newRoomTemplate]);
            createdRoom = newRoom.GetComponent<RoomManager>();
            createdRoom.roomValidationScript.id = i;
            // Position correctly according to exit
            newRoom.transform.position = nextSpot;
            newRoom.transform.eulerAngles = nextRotation;
            // Save data for next room
            nextSpot = createdRoom.exit.position;
            nextRotation = createdRoom.exit.eulerAngles;
            //nextRotation.y = nextRotation.y-180;
            // Cache In the Room
            currentRooms.Add(createdRoom);
            lastRoomTemplate = createdRoom.roomTemplate;
        }
    }
    void GenerateRooms(int startingId)
    {
        //Debug.Log("Generating Rooms after Error. ID = " + startingId);

        Vector3 nextSpot = currentRooms[startingId].exit.position;
        Vector3 nextRotation = currentRooms[startingId].exit.eulerAngles;
        GameObject newRoom;
        RoomManager createdRoom;
        int lastRoomTemplate = -currentRooms[startingId].roomTemplate;
        for (int i = startingId +1; i < roomsToGenerate; i++)
        {
            int newRoomTemplate = GetRandomRoomExcept(lastRoomTemplate);
            newRoom = Instantiate(possibleRooms[newRoomTemplate]);
            createdRoom = newRoom.GetComponent<RoomManager>();
            createdRoom.roomValidationScript.id = i;
            // Position correctly according to exit
            newRoom.transform.position = nextSpot;
            newRoom.transform.eulerAngles = nextRotation;
            // Save data for next room
            nextSpot = createdRoom.exit.position;
            nextRotation = createdRoom.exit.eulerAngles;
            //nextRotation.y = nextRotation.y-180;
            // Cache In the Room
            currentRooms.Add(createdRoom);
            lastRoomTemplate = createdRoom.roomTemplate;
        }
        iteration++;
    }
    int GetRandomRoomExcept(int x)
    {
        int val = Random.Range(0, possibleRooms.Count);
        while (val == x)
        {
            val = Random.Range(0, possibleRooms.Count);
        }
        return val;
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

    void Foo()
    {
        GameObject room = null;

        Vector3 rot = room.transform.eulerAngles;
        GameObject newroom = null;
        newroom.transform.eulerAngles = rot;
    }
}
