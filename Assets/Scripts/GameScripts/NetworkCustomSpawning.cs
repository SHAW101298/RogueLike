using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine.Networking;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Services.Lobbies;


public class NetworkCustomSpawning : NetworkBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene Manager says scene is loaded = " + arg0.name);
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadComplete += LoadingCompleted;
    }

    private void LoadingCompleted(ulong clientID, string sceneName, LoadSceneMode loadSceneMode)
    {
        // This is called on SERVER, when a client loads.
        if(sceneName != "SampleScene")
        {
            return;
        }
        Debug.Log("I completed Loading. my clientID is = " + clientID);
        // Ask client for his spawning data

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientID }
            }
        };

        LoadComplete_ClientRPC(clientRpcParams);
    }

    [ClientRpc]
    public void LoadComplete_ClientRPC(ClientRpcParams clientparams)
    {
        Debug.Log("Load Completed for you");
        //int character = Convert.ToInt32(LobbyManager.Instance.currentPlayer.Data["Character"].Value);
        //SpawnMe_ServerRPC(character);
    }






    private void SpawnPrefabs(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.Log("Connected Clients = " + clientsCompleted.Count);
        foreach(ulong id in clientsCompleted)
        {
            Debug.Log("Im connected ! = " + id);
        }
        //LoadComplete_ClientRPC();
        /*
        Debug.LogWarning("Sample Scene !!");
        if(IsHost == true && sceneName == "SampleScene")
        {
            GameObject temp;
            foreach(ulong client in clientsCompleted)
            {
                Debug.Log("client = " + client);
                //Debug.Log("lobby client allocation id= " + LobbyManager.Instance.currentPlayer.AllocationId);
                //Debug.Log("lobby client id= " + LobbyManager.Instance.currentPlayer.Id);
                //Debug.Log("Lobby client connection info" + LobbyManager.Instance.currentPlayer.ConnectionInfo);
                //Debug.Log(LobbyManager.Instance.currentLobby.Players[].);
                temp = Instantiate(prefab);
                temp.GetComponent<NetworkObject>().SpawnWithOwnership(client, true);
                // https://forum.unity.com/threads/how-do-i-link-lobby-players-to-unity-ngo-networkobjects.1360111/
                Debug.Log("There ?");
            }
        }

        */
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
