using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_MainMenu : MonoBehaviour
{
    [Header("REF")]
    [SerializeField] LobbyManager lobbyManager;
    [Header("Windows")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] UI_Window menuWindow;
    [SerializeField] UI_Window playWindow;
    [SerializeField] UI_Window optionsWindow;
    [SerializeField] UI_Window exitWindow;
    [Space(10)]
    [SerializeField] UI_Window createLobbyWindow;
    [SerializeField] UI_Window joinLobbyWindow;
    [SerializeField] UI_Window joinLobbyByCodeWindow;
    [SerializeField] UI_Window joinLobbyFromListWindow;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    public void BTN_CreateLobbyCREATE()
    {
        lobbyManager.CallCreateLobby();
    }
    public void BTN_CreateLobbyReturn()
    {
        createLobbyWindow.CloseWindow();
        lobbyManager.ResetInputData();
        createLobbyWindow.parent.OpenWindow();
    }
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

    public void BTN_JoinLobbyFromList()
    {
        joinLobbyFromListWindow.OpenWindow();
        joinLobbyFromListWindow.parent.CloseWindow();
    }
    public void BTN_JoinLobbyFromListJOIN()
    {

    }
    public void BTN_JoinLobbyFromListReturn()
    {
        joinLobbyFromListWindow.CloseWindow();
        joinLobbyFromListWindow.parent.OpenWindow();
    }
    public void BTN_JoinLobbyByCode()
    {
        joinLobbyByCodeWindow.OpenWindow();
        joinLobbyByCodeWindow.parent.CloseWindow();
    }
    public void BTN_JoinLobbyByCodeJOIN()
    {
        lobbyManager.CallJoinLobby();
    }
    public void BTN_JoinLobbyByCodeReturn()
    {
        joinLobbyByCodeWindow.CloseWindow();
        joinLobbyByCodeWindow.parent.OpenWindow();
    }
    
}
