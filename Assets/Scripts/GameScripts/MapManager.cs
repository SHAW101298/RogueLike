using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public RoomManager Spawn;
    public GameObject mapParent;
    public List<RoomManager> floorSpawns;
    public List<GameObject> floorParents;
    public List<RoomGenerator> floorGenerators;


    public void ActivateFloor(int floorNumber)
    {
        
    }
    public void GenerateFloors()
    {
        foreach (RoomGenerator generator in floorGenerators)
        {
            generator.FirstRoomGeneration();
        }
    }
    public void RequestMapLayout()
    {
        for(int i = 0; i < floorGenerators.Count; i++)
        {
            floorGenerators[i].RequestMapLayout_ServerRPC(NetworkManager.Singleton.LocalClientId);
        }
    }

}
