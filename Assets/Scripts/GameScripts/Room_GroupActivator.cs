using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Room_GroupActivator : MonoBehaviour
{
    [SerializeField] Room_SpawningData group;


    private void Start()
    {
        if(NetworkManager.Singleton.IsHost == false)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            group.NotifyAboutPlayer(other.gameObject);
            Destroy(gameObject);
        }
    }


}
