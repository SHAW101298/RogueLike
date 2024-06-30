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
    [SerializeField] UI_JoinLobbyByCode ui_JoinByCode;
    [SerializeField] UI_CreateLobby ui_CreateLobby;

    [Header("Windows")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] UI_Window menuWindow;
    [SerializeField] UI_Window playWindow;
    [SerializeField] UI_Window optionsWindow;
    [SerializeField] UI_Window exitWindow;
    [SerializeField] UI_Window changeNameWindow;
    [SerializeField] UI_Window lobbyWindow;
    [Space(10)]
    [SerializeField] UI_Window createLobbyWindow;
    [SerializeField] UI_Window joinLobbyWindow;
    [SerializeField] UI_Window joinLobbyByCodeWindow;
    [SerializeField] UI_Window joinLobbyFromListWindow;


    private void Start()
    {
        lobbyManager = LobbyManager.Instance;
        relayManager = RelayManager.Instance;
    }

    public void BTN_Play()
    {
        playWindow.OpenWindow();
        playWindow.parent.CloseWindow();
    }
    public void BTN_PlayReturn()
    {
        playWindow.CloseWindow();
        playWindow.parent.OpenWindow();
    }
    public void BTN_Options()
    {
        optionsWindow.OpenWindow();
        optionsWindow.parent.CloseWindow();
    }
    public void BTN_OptionsReturn()
    {
        optionsWindow.CloseWindow();
        optionsWindow.parent.OpenWindow();
    }
    public void BTN_Exit()
    {
        exitWindow.OpenWindow();
        exitWindow.parent.CloseWindow();
    }
    public void BTN_ExitNo()
    {
        exitWindow.CloseWindow();
        exitWindow.parent.OpenWindow();
    }
    public void BTN_ExitYes()
    {
        Application.Quit();
    }

    // ====================================

    public void BTN_CreateLobby()
    {
        createLobbyWindow.OpenWindow();
        createLobbyWindow.parent.CloseWindow();
        ui_CreateLobby.ResetInputData();
    }

    // ====================================

    public void BTN_JoinLobby()
    {
        joinLobbyWindow.OpenWindow();
        joinLobbyWindow.parent.CloseWindow();
    }
    public void BTN_JoinLobbyReturn()
    {
        joinLobbyWindow.CloseWindow();
        joinLobbyWindow.parent.OpenWindow();
    }
    // ====================================

    // ====================================
    
    public void BTN_JoinLobbyByCode()
    {
        joinLobbyByCodeWindow.OpenWindow();
        joinLobbyByCodeWindow.parent.CloseWindow();
    }
    public void BTN_JoinLobbyFromList()
    {
        joinLobbyFromListWindow.OpenWindow();
        joinLobbyFromListWindow.parent.CloseWindow();
    }
    // ====================================
    public void HideJoiningWindows()
    {
        joinLobbyByCodeWindow.CloseWindow();
        joinLobbyFromListWindow.CloseWindow();
        createLobbyWindow.CloseWindow();
    }
    public void ShowMenuWindow()
    {
        menuWindow.OpenWindow();
    }
    public async void BTN_StartGame()
    {
        if(lobbyManager.ReturnIsHost() == true)
        {     
            string relayCode = await relayManager.CreateRelay();
            if(relayCode == "")
            {
                UI_ErrorHandler.instance.ShowErrorMessage("Relay code not returned. Relay couldn't be created");
                return;
            }
            lobbyManager.UpdateLobbyWithRelayCode(relayCode);
        }
    }
    public void HideLobbyWindow()
    {
        lobbyWindow.CloseWindow();
    }
}
