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
using UnityEditor.PackageManager;


public class NetworkCustomSpawning : NetworkBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(this);
        
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += LoadingCompletedForAll;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += LoadingCompleted;
        
    }

    private void LoadingCompletedForAll(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (sceneName != "SampleScene")
            return;

        Debug.Log("Host now sees everyone has been connected");
        Debug.Log("Amount of clients completed = " + clientsCompleted.Count);
        Debug.Log("Amount of client timedOut = " + clientsTimedOut.Count);
        foreach(ulong clientCompleted in clientsCompleted)
        {
            Debug.Log("client = " + clientCompleted + " is finished");
        }
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

    // Wrong !!
    [ServerRpc(RequireOwnership = false)]
    public void SpawnMe_ServerRPC(int character, ulong clientID)
    {
        Debug.Log("Player is requesting a spawn of character = " + character);
        Debug.Log("Sender client id = " + clientID);

        
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
