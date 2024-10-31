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
        SetAsRelayTransport();
        await PrepareLobby(lobbyData);
        await Task.Delay(2);
        await PrepareRelay();
        await Task.Delay(2);
        NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);


        //ui_MainMenu.HideLobbyWindow();
        //ui_MainMenu.BTN_MultiplayerReturn();
        //ui_MainMenu.ShowMenuWindow();


    }
    public void JoinGameOnRelay(string inviteCode)
    {
        lobbyManager.CallJoinLobbyByCode(inviteCode);
        relayManager.JoinRelay(inviteCode);
    }
    public void StartGameAsOffline()
    {
        SetAsUnityTransport();
        NetworkManager.Singleton.StartHost();
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
    async Task<int> PrepareRelay()
    {
        await relayManager.CreateRelay();
        return 1;
    }
}
