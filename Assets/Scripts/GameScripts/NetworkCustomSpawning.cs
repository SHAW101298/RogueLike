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
using Unity.Mathematics;
using System.Security.Cryptography;


public class NetworkCustomSpawning : NetworkBehaviour
{
    #region
    public static NetworkCustomSpawning Instance;
    private void Awake()
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
    [SerializeField] int expectedAmountOfPlayers;
    public int receivedCallBacks;
    [SerializeField] List<GameObject> characterPrefabs;

    private void Start()
    {
        Debug.Log("Network Custom Spawning START");
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene Manager says scene is loaded = " + arg0.name);
        LobbyManager.Instance.CallMarkAsLoaded();
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadComplete += LoadingCompleted;
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadEventCompleted;
    }

    private void OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {

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
        receivedCallBacks++;

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientID }
            }
        };

        if(receivedCallBacks >= expectedAmountOfPlayers)
        {
            Debug.Log("YEY, everyone is loaded !");
            PromptPlayersForSpawnCommand_ClientRPC();
        }
        //LoadComplete_ClientRPC(clientRpcParams);




    }

    [ClientRpc]
    public void LoadComplete_ClientRPC(ClientRpcParams clientparams)
    {
        Debug.Log("Load Completed for you");
        //int character = Convert.ToInt32(LobbyManager.Instance.currentPlayer.Data["Character"].Value);
        //SpawnMe_ServerRPC(character);
    }
    [ClientRpc]
    public void PromptPlayersForSpawnCommand_ClientRPC()
    {
        Debug.Log("Server Asks you to pass spawning data");
        int character = Convert.ToInt32(LobbyManager.Instance.currentPlayer.Data["Character"].Value);
        //character = RandomNumberGenerator.GetInt32(9);
        SpawnData_ServerRPC(character);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SpawnData_ServerRPC(int character, ServerRpcParams serverRpcParams = default)
    {
        Debug.Log("Requesting spawning of character | " + character + " | for " + serverRpcParams.Receive.SenderClientId);
        Debug.Log("Sender Client ID = " + serverRpcParams.Receive.SenderClientId);

        if(IsHost == false)
        {
            return;
        }

        GameObject temp;
        temp = Instantiate(characterPrefabs[character]);
        temp.GetComponent<NetworkObject>().SpawnAsPlayerObject(serverRpcParams.Receive.SenderClientId, true);
        temp.GetComponent<NetworkObject>().ChangeOwnership(serverRpcParams.Receive.SenderClientId);
        temp.transform.position = Vector3.zero;
        temp.transform.localEulerAngles = Vector3.zero;
    }

    public void SetAmountOfExpectedPlayers(int amount)
    {
        expectedAmountOfPlayers = amount;
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
