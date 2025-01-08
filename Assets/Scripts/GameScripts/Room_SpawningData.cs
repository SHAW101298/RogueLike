using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Room_SpawningData : MonoBehaviour
{
    [SerializeField] List<GameObject> units;
    public void SpawnUnits()
    {
        //Debug.Log("Spawning enemies");
        GameObject unit_GO;
        int i = 0;
        foreach(GameObject unit in units)
        {
            //Debug.Log("Desired position = " + gameObject.transform.position);
            unit_GO = Instantiate(unit, gameObject.transform.position, gameObject.transform.rotation);
            unit_GO.GetComponent<NetworkObject>().Spawn();
        }
    }
}
