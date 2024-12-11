using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
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
    public NavMeshSurface surface;
    //public Transform spawnAreaExit;
    public List<GameObject> possibleRooms;
    public List<RoomManager> currentRooms;
    public int roomsToGenerate = 4;
    public int maxIteration = 100;
    public float amountToDelay;
    [Space(20)]
    public int earliestError = -1;
    public bool regenerateRooms;
    public int iteration = 0;
    public float timer = 1;
    public bool navMeshBuiltAlready;

    [Header("Debug")]
    public bool generateRooms;
    public bool clearRooms;

    private void Update()
    {
        if (IsOwner == false)
            return;


        if(generateRooms == true)
        {
            DestroyRoomsExceptSpawn();
            generateRooms = false;
            FirstRoomGeneration();
            DelayNavBaking();
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
            DestroyRoomsExceptSpawn();
        }

        BuildNavMesh();
    }

    

    public void FirstRoomGeneration()
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
        if (IsOwner == false)
            return;

        //Debug.Log("Error on ID " + id);
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
        //Debug.Log("============================\n\n");
        //Debug.Log("Destroying Rooms from " + earliestError);
        DestroyRoomsToError();
        GenerateRooms(earliestError);
        earliestError = -1;
    }

    private void DestroyRoomsToError()
    {
        //Debug.Log("Destroying rooms until = " + earliestError);
        for (int i = currentRooms.Count - 1; i > earliestError; i--)
        {
            Destroy(currentRooms[i].gameObject);
            currentRooms.RemoveAt(i);
            DelayNavBaking();
        }
    }
    private void DestroyRoomsExceptSpawn()
    {
        for (int i = currentRooms.Count - 1; i > 0; i--)
        {
            Destroy(currentRooms[i].gameObject);
            currentRooms.RemoveAt(i);
        }
    }

    void DelayNavBaking()
    {
        timer = amountToDelay;
    }
    void BuildNavMesh()
    {
        if (navMeshBuiltAlready == true)
            return;

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Debug.Log("Building NavMesh");
            surface.BuildNavMesh();
            navMeshBuiltAlready = true;
            GameSetup.Instance.CreateMapForOtherPlayers();
        }
    }
    string GetMapLayout()
    {
        string layout = "";

        for(int i = 1; i < currentRooms.Count; i++)
        {
            layout += currentRooms[i].roomTemplate.ToString();
        }

        return layout;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestMapLayout_ServerRPC(ulong requestingPlayer)
    {
        string layout = GetMapLayout();
        RequestMapLayout_ClientRPC(layout, requestingPlayer);
    }
    [ClientRpc]
    void RequestMapLayout_ClientRPC(string layout,ulong requestingPlayer)
    {
        if(requestingPlayer == NetworkManager.Singleton.LocalClientId)
        {
            Debug.LogWarning("CREATE MAP");
            CreateRoomsFromLayout(layout);
        }
    }
    void CreateRoomsFromLayout(string layout)
    {
        currentRooms.Add(spawnArea);
        Vector3 nextSpot = spawnArea.exit.position;
        Vector3 nextRotation = spawnArea.exit.eulerAngles;
        GameObject newRoom;
        RoomManager createdRoom;

        Debug.Log("Layout is = " + layout);
        for(int i = 0; i < layout.Length; i++)
        {
            newRoom = Instantiate(possibleRooms[layout[i]]);
            createdRoom = newRoom.GetComponent<RoomManager>();
            // Position correctly according to exit
            newRoom.transform.position = nextSpot;
            newRoom.transform.eulerAngles = nextRotation;
            // Save data for next room
            nextSpot = createdRoom.exit.position;
            nextRotation = createdRoom.exit.eulerAngles;
            // Cache In the Room
            currentRooms.Add(createdRoom);
        }
    }
}
