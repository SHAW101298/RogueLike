using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;
using Unity.Netcode;



public class UI_MainMenu : MonoBehaviour
{
    #region
    public static UI_MainMenu instance;
    public void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion
    [Header("REF")]
    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] RelayManager relayManager;
    [SerializeField] UI_Lobby ui_Lobby;
    [SerializeField] UI_LobbyList ui_LobbyList;
    [SerializeField] NetworkTypeController networkTypeController;
    [SerializeField] UI_GameOptions ui_gameOptions;
    //[SerializeField] UI_JoinLobbyByCode ui_JoinByCode;
    //[SerializeField] UI_CreateLobby ui_CreateLobby;

    [Header("Windows")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] UI_Window menuWindow;
    [SerializeField] UI_Window singlePlayerWindow;
    [SerializeField] UI_Window multiPlayerWindow;
    [SerializeField] UI_Window gameOptionsWindowSinglePlayer;
    [SerializeField] UI_Window gameOptionsWindowMultiPlayer;
    [SerializeField] UI_Window charactersWindow;
    [SerializeField] UI_Window optionsWindow;
    [SerializeField] UI_Window exitWindow;
    

    [SerializeField] UI_Window changeNameWindow;
    [SerializeField] UI_Window lobbyWindow;

    [Header("Fields")]
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown windowModeDropdown;



    private void Start()
    {
        lobbyManager = LobbyManager.Instance;
        relayManager = RelayManager.Instance;
        networkTypeController = NetworkTypeController.Instance;
        Debug.LogWarning("ZMIENIC BACKGROUND. COPYRIGHT");
    }

    public void BTN_SinglePlayer()
    {
        singlePlayerWindow.OpenWindow();
        menuWindow.CloseWindow();
        //NetworkManager.Singleton.gameObject.GetComponent<NetworkTypeController>().SetAsUnityTransport();
    }
    public void BTN_SinglePlayerReturn()
    {
        singlePlayerWindow.CloseWindow();
        menuWindow.OpenWindow();
    }
    public void BTN_Multiplayer()
    {
        multiPlayerWindow.OpenWindow();
        menuWindow.CloseWindow();
        //NetworkManager.Singleton.gameObject.GetComponent<NetworkTypeController>().SetAsRelayTransport();
    }
    public void BTN_MultiplayerReturn()
    {
        multiPlayerWindow.CloseWindow();
        menuWindow.OpenWindow();
    }
    public void BTN_Characters()
    {
        charactersWindow.OpenWindow();
        menuWindow.CloseWindow();
    }
    public void BTN_CharactersReturn()
    {
        charactersWindow.CloseWindow();
        menuWindow.OpenWindow();
    }
    public void BTN_Options()
    {
        optionsWindow.OpenWindow();
        menuWindow.CloseWindow();
    }
    public void BTN_OptionReturn()
    {
        optionsWindow.CloseWindow();
        menuWindow.OpenWindow();
    }
    public void BTN_Exit()
    {
        exitWindow.OpenWindow();
        menuWindow.CloseWindow();
    }
    public void BTN_ExitNo()
    {
        exitWindow.CloseWindow();
        menuWindow.OpenWindow();
    }
    public void BTN_ExitYes()
    {
        if(lobbyManager.currentLobby != null)
        {
            lobbyManager.CallLeaveLobby();
            lobbyManager.currentLobby = null;
        }

        Application.Quit();
    }

    // ====================================

    // ====================================

    // ====================================

    // ====================================
    
    // ====================================

    public void ShowMenuWindow()
    {
        menuWindow.OpenWindow();
    }
    public async void BTN_StartGame()
    {

        UI_ErrorHandler.instance.ShowErrorMessage("Old Ways. Returning");
        return;

        if(lobbyManager.ReturnIsHost() == true)
        {     
            string relayCode = await relayManager.CreateRelay();
            if(relayCode == "")
            {
                UI_ErrorHandler.instance.ShowErrorMessage("Relay code not returned. Relay couldn't be created");
                return;
            }
            //lobbyManager.UpdateLobbyWithRelayCode(relayCode);
        
        }
        
    }
    public void BTN_ShowGameOptionsWindow_Multiplayer()
    {
        gameOptionsWindowMultiPlayer.OpenWindow();
    }
    public void BTN_ShowGameOptionsWindow_Singleplayer()
    {
        gameOptionsWindowSinglePlayer.OpenWindow();
    }
    public void HideGameOptionsWindow()
    {
        gameOptionsWindowMultiPlayer.CloseWindow();
        gameOptionsWindowSinglePlayer.CloseWindow();
    }
    public void BTN_StartOFflineGame()
    {
        ui_gameOptions.ReadGameOptions();
        networkTypeController.StartGameAsOffline();
    }
    public void HideLobbyWindow()
    {
        lobbyWindow.CloseWindow();
    }

    public void BTN_ApplySettings()
    {
        bool fullscreen;
        float width, height;
        if (windowModeDropdown.value == 0)
        {
            fullscreen = true;
        }
        else
        {
            fullscreen = false;
        }

        switch(resolutionDropdown.value)
        {
            case 0:
                width = 1920;
                height = 1080;
                break;
            case 1:
                width = 1600;
                height = 900;
                break;
            case 2:
                width = 1366;
                height = 768;
                break;
            case 3:
                width = 1360;
                height = 768;
                break;
            case 4:
                width = 1280;
                height = 720;
                break;
            case 5:
                width = 1024;
                height = 768;
                break;
            case 6:
                width = 800;
                height = 600;
                break;
            default:
                width = Screen.currentResolution.width;
                height = Screen.currentResolution.height;
                break;
        }
        Screen.fullScreen = fullscreen;

        BTN_OptionReturn();
    }
}
