using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<EnemyData> enemiesInRoom;
    [Space(15)]
    [SerializeField] Transform spawnPosition;


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
        // Activate GameObject
        // Teleport Player
        // Make enemies Seek players out
    }
    public void EVENT_PlayerEnteringPortal()
    {
        // Check if room is already active

        //ActivateRoom();
    }
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition.position;
    }    
}
