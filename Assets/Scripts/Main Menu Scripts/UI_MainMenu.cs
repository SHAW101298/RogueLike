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
    //[SerializeField] UI_JoinLobbyByCode ui_JoinByCode;
    //[SerializeField] UI_CreateLobby ui_CreateLobby;

    [Header("Windows")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] UI_Window menuWindow;
    [SerializeField] UI_Window singlePlayerWindow;
    [SerializeField] UI_Window multiPlayerWindow;
    [SerializeField] UI_Window charactersWindow;
    [SerializeField] UI_Window optionsWindow;
    [SerializeField] UI_Window exitWindow;

    [SerializeField] UI_Window changeNameWindow;
    [SerializeField] UI_Window lobbyWindow;



    private void Start()
    {
        lobbyManager = LobbyManager.Instance;
        relayManager = RelayManager.Instance;
        Debug.LogWarning("ZMIENIC BACKGROUND. COPYRIGHT");
    }

    public void BTN_SinglePlayer()
    {
        singlePlayerWindow.OpenWindow();
        menuWindow.CloseWindow();
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
