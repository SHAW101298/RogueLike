using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Room_SpawningData : MonoBehaviour
{
    [SerializeField] List<GameObject> units;
    [SerializeField] RoomManager room;
    [SerializeField] List<EnemyData> spawnedUnits;
    public void SpawnUnits()
    {
        //Debug.Log("Spawning enemies");
        GameObject unit_GO;
        EnemyData unit_Data;
        int i = 0;
        foreach(GameObject unit in units)
        {
            Debug.Log("Desired position = " + gameObject.transform.position);
            unit_GO = Instantiate(unit, gameObject.transform.position, gameObject.transform.rotation);
            unit_GO.GetComponent<NetworkObject>().Spawn();
            unit_Data = unit_GO.GetComponent<EnemyData>();
            room.enemiesInRoom.Add(unit_Data);
            spawnedUnits.Add(unit_Data);
        }
    }
    public void NotifyAboutPlayer(GameObject playerObject)
    {
        PlayerData player = playerObject.GetComponent<PlayerData>();
        foreach (EnemyData enemy in spawnedUnits)
        {
            enemy.ai.NotifyAboutPlayer(player);
        }
    }
}
