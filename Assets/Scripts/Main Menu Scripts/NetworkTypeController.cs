using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Networking;

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

    [SerializeField] UnityTransport relayTransport;
    [SerializeField] UnityTransport unityTransport;
    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] RelayManager relayManager;

    [Space(20)]
    [SerializeField] UI_MainMenu ui_MainMenu;


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

    public void StartGameAsRelay()
    {
        SetAsRelayTransport();
        Debug.Log("We received Relay Code");

        bool ishost = ReturnIsHost();

        /*
        countdownTimer += Time.deltaTime;
        if(countdownTimer < 1)
        {
            //return;
        }
        */

        if (ishost == false) // Host is already in the relay
        {
            relayManager.JoinRelay(lobbyManager.currentLobby.Data["Key_Game_Start"].Value);
        }
        //currentLobby = null; // Will destroy current lobby after 30 seconds
        ui_MainMenu.HideLobbyWindow();
        ui_MainMenu.BTN_MultiplayerReturn();
        ui_MainMenu.ShowMenuWindow();

        //NetworkCustomSpawning.Instance.SetAmountOfExpectedPlayers(currentLobby.Players.Count);
        // Clients will be forced to change scene
        if (ishost == true)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
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
}
