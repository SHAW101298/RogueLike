using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Networking;


public class LobbyCreationData
{
    public string name;
    public string players;
    public bool isPrivate;
    public string password;
}


public class NetworkTypeController : NetworkBehaviour
{
    public static NetworkTypeController Instance;
    public void Awake()
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

    public UnityTransport relayTransport;
    public UnityTransport unityTransport;
    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] RelayManager relayManager;

    [Space(20)]
    [SerializeField] UI_MainMenu ui_MainMenu;


    private void Start()
    {
        DontDestroyOnLoad(this);
        ui_MainMenu = UI_MainMenu.instance;
    }

    public void SetAsRelayTransport()
    {
        Debug.Log("Setting As Relay");
        NetworkManager.Singleton.NetworkConfig.NetworkTransport = relayTransport;
    }
    public void SetAsUnityTransport()
    {
        Debug.Log("Setting As Unity");
        NetworkManager.Singleton.NetworkConfig.NetworkTransport = unityTransport;
    }

    public async void HostGameAsRelay(LobbyCreationData lobbyData)
    {
        LoadingInfo.Instance.EnableInfo();
        string relayCode = "";
        LoadingInfo.Instance.UpdateCurrentProgress("Changing Transport Type");
        SetAsRelayTransport();
        LoadingInfo.Instance.UpdateCurrentProgress("Preparing Lobby");
        await PrepareLobby(lobbyData);
        await Task.Delay(2);
        LoadingInfo.Instance.UpdateCurrentProgress("Preparing Relay");
        relayCode = await PrepareRelay();
        await Task.Delay(1);
        LoadingInfo.Instance.UpdateCurrentProgress("Updating Lobby with join code");
        lobbyManager.UpdateLobbyWithRelayCode(relayCode);
        await Task.Delay(1);
        LoadingInfo.Instance.UpdateCurrentProgress("Changing Scene");
        NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);


        //ui_MainMenu.HideLobbyWindow();
        //ui_MainMenu.BTN_MultiplayerReturn();
        //ui_MainMenu.ShowMenuWindow();


    }
    public void JoinGameOnRelayByCode(string inviteCode)
    {
        lobbyManager.CallJoinLobbyByCode(inviteCode);
        relayManager.JoinRelay(inviteCode);
    }
    public async void JoinGameOnRelayByID(string lobbyId)
    {
        Debug.Log("Calling to join a lobby");
        lobbyManager.CallJoinLobbyByID(lobbyId);
        Debug.Log("Starting 2 sec wait");
        await Task.Delay(1000);
        Debug.Log("Finished Waiting");
        Debug.Log("Current lobby id = " + lobbyManager.currentLobby.Id);
        Debug.Log("Key Value = " + lobbyManager.currentLobby.Data["Key_Game_Start"].Value);
        string relayCode = lobbyManager.currentLobby.Data["Key_Game_Start"].Value;
        await Task.Delay(500);
        relayManager.JoinRelay(relayCode);
    }
    public async void JoinGameOnRelayByID(string lobbyId, string password)
    {
        Debug.Log("Calling to join a lobby");
        lobbyManager.CallJoinLobbyByID(lobbyId, password);
        Debug.Log("Starting 2 sec wait");
        await Task.Delay(1000);
        Debug.Log("Finished Waiting");
        Debug.Log("Current lobby id = " + lobbyManager.currentLobby.Id);
        Debug.Log("Key Value = " + lobbyManager.currentLobby.Data["Key_Game_Start"].Value);
        string relayCode = lobbyManager.currentLobby.Data["Key_Game_Start"].Value;
        await Task.Delay(500);
        relayManager.JoinRelay(relayCode);
    }
    public void StartGameAsOffline()
    {
        LoadingInfo.Instance.EnableInfo();
        LoadingInfo.Instance.UpdateCurrentProgress("Changing Transport Type");
        SetAsUnityTransport();
        LoadingInfo.Instance.UpdateCurrentProgress("Starting host");
        NetworkManager.Singleton.StartHost();
        LoadingInfo.Instance.UpdateCurrentProgress("Changing Scene");
        NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);

    }

    public bool ReturnIsHost()
    {
        if (lobbyManager.currentLobby.HostId == AuthenticationService.Instance.PlayerId)
        {
            return true;
        }
        return false;
    }

    async Task<int> PrepareLobby(LobbyCreationData lobbyData)
    {
        await lobbyManager.CreateLobby(lobbyData);
        //lobbyManager.CallCreateLobby(lobbyData);
        return 1;
    }
    async Task<string> PrepareRelay()
    {
        string relayCode = "";
        relayCode = await relayManager.CreateRelay();
        return relayCode;
    }
}
