using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Netcode;
using UnityEngine;

public class RoomGenerator : NetworkBehaviour
{
    #region
    /*
    public static RoomGenerator Instance;
    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            //Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    */
    #endregion
    public RoomManager spawnArea;
    public NavMeshSurface surface;
    public List<GameObject> possibleRooms;
    public List<RoomManager> currentRooms;
    public GameObject roomsParent;

    [Space(10)]
    public int roomsToGenerate = 4;
    public int maxIteration = 100;
    public float amountToDelay;
    [Space(20)]
    public int earliestError = -1;
    public bool regenerateRooms;
    public int iteration = 0;
    public float timer = 1;
    public bool navMeshBuiltAlready;

    [Header("Random Map Objects")]
    public List<BreakAbles> breakables;
    public List<GameObject> specialInteractables;
    public List<GameObject> challengeRooms;
    public int breakableSpawnChance;


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
            navMeshBuiltAlready = false;
            iteration = 0;
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
            createdRoom.floorParent = this;
            // Position correctly according to exit
            newRoom.transform.position = nextSpot;
            newRoom.transform.eulerAngles = nextRotation;
            newRoom.transform.SetParent(roomsParent.transform);
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
        int lastRoomTemplate = currentRooms[startingId-1].roomTemplate;
        //Debug.Log("Last room template = " + lastRoomTemplate);
        for (int i = startingId +1; i < roomsToGenerate; i++)
        {
            int newRoomTemplate = GetRandomRoomExcept(lastRoomTemplate);
            newRoom = Instantiate(possibleRooms[newRoomTemplate]);
            createdRoom = newRoom.GetComponent<RoomManager>();
            //Debug.Log("NEW ROOM TEMPLATE = " + createdRoom.roomTemplate);
            createdRoom.roomValidationScript.id = i;
            createdRoom.floorParent = this;
            // Position correctly according to exit
            newRoom.transform.position = nextSpot;
            newRoom.transform.eulerAngles = nextRotation;
            newRoom.transform.SetParent(roomsParent.transform);
            // Save data for next room
            nextSpot = createdRoom.exit.position;
            nextRotation = createdRoom.exit.eulerAngles;
            //nextRotation.y = nextRotation.y-180;
            // Cache In the Room
            currentRooms.Add(createdRoom);
            lastRoomTemplate = createdRoom.roomTemplate;
            //Debug.Log("SET lastRoomTemplate = " + lastRoomTemplate);
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
        //Debug.Log("Earliest Error is " + earliestError);
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
            if(i == 0)
            {
                Debug.LogWarning("I is = " + i);
                continue;
            }
            //Debug.Log("I = " + i + " \n" + currentRooms[i].gameObject.name);
            currentRooms[i].DestroyEnemiesInRoom();
            Destroy(currentRooms[i].gameObject);
            currentRooms.RemoveAt(i);
        }
        currentRooms.Clear();
    }

    void DelayNavBaking()
    {
        //Debug.Log("Delaying Nav Baking");
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
            //SpawnEnemiesInRooms();
            GameOptions.Instance.ApplyDifficultySettings();

            DeactivateFloorForMe();
            ActivateFirstRooms();
            GenerateBreakAblesInRooms();
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
        int template;
        string templateString = "";

        //Debug.Log("Layout is = " + layout);
        for(int i = 0; i < layout.Length; i++)
        {
            //Debug.Log("i is = " + i);
            templateString += layout[i];
            template = int.Parse(templateString);
            //Debug.Log("Char is " + layout[i]);
            //Debug.Log("Template is " + template);
            newRoom = Instantiate(possibleRooms[template]);
            createdRoom = newRoom.GetComponent<RoomManager>();
            // Position correctly according to exit
            newRoom.transform.position = nextSpot;
            newRoom.transform.eulerAngles = nextRotation;
            newRoom.transform.SetParent(roomsParent.transform);
            // Save data for next room
            nextSpot = createdRoom.exit.position;
            nextRotation = createdRoom.exit.eulerAngles;
            // Cache In the Room
            currentRooms.Add(createdRoom);
            templateString = "";
        }

        foreach(RoomManager room in currentRooms)
        {
            room.gameObject.SetActive(false);
        }

    }
    [ServerRpc(RequireOwnership = false)]
    void RequestRoomBreakablesLayout_ServerRPC(ulong requestingPlayer)
    {

    }

    public void ActivateFloorForMe()
    {
        /*
        foreach(RoomManager room in currentRooms)
        {
            room.gameObject.SetActive(true);
        }
        */
        roomsParent.SetActive(true);
    }
    public void DeactivateFloorForMe()
    {     
        Debug.Log("Deactivating Whole floor");
        foreach(RoomManager room in currentRooms)
        {
            room.gameObject.SetActive(true);
        }
    }
    public void ActivateFirstRooms()
    {
        Debug.Log("Activating First Rooms");
        currentRooms[0].ActivateRoom();
        currentRooms[1].ActivateRoom();
    }

    public void GenerateBreakAblesInRooms()
    {
        Debug.Log("Generating Breakables In Rooms");
        for(int i = 0; i < currentRooms.Count; i++)
        {
            currentRooms[i].GenerateBreakablesInRoom();
            //CreateBreakAblesInRooms(currentRooms[i]);
        }
    }
}
