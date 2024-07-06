using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine.Networking;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class NetworkCustomSpawning : NetworkBehaviour
{
    GameObject prefab;

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SpawnPrefabs;
    }

    private void SpawnPrefabs(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.LogWarning("Sample Scene !!");
        if(IsHost == true && sceneName == "SampleScene")
        {
            GameObject temp;
            foreach(ulong client in clientsCompleted)
            {
                temp = Instantiate(prefab);
                temp.GetComponent<NetworkObject>().SpawnWithOwnership(client, true);
                // https://forum.unity.com/threads/how-do-i-link-lobby-players-to-unity-ngo-networkobjects.1360111/
                Debug.Log("There ?")
            }
        }
    }



    /*
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);

        GameObject player;
        Transform startPos = GetStartPosition();

        if (startPos != null)
        {
            player = Instantiate(characters[chosenCharacter], startPos.position, startPos.rotation) as GameObject;
        }
        else
        {
            player = Instantiate(characters[chosenCharacter], Vector3.zero, Quaternion.identity) as GameObject;

        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }
    */
}
