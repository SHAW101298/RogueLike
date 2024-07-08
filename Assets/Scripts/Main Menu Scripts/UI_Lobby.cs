using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using System.Runtime.CompilerServices;

public class UI_Lobby : MonoBehaviour
{
    #region
    public static UI_Lobby instance;
    private void Awake()
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

    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] UI_MainMenu ui_MainMenu;
    [SerializeField] GameObject playerDataPrefab;
    [SerializeField] Transform currentPlayersContent;
    [Header("Windows")]
    [SerializeField] GameObject lobbyWindow;
    [SerializeField] GameObject joinCodeWindow;
    [SerializeField] GameObject characterSelectWindow;
    [Header("Text Fields")]
    [SerializeField] TMP_Text currentLobbyName;
    [SerializeField] TMP_Text lobbyJoinCode;
    [Header("Colors")]
    [SerializeField] Color redColor;
    [SerializeField] Color greenColor;
    [SerializeField] GameObject optionsButton;



    public void ActivateLobbyWindow()
    {
        lobbyWindow.gameObject.SetActive(true);
        currentLobbyName.text = lobbyManager.currentLobby.Name;
        if(lobbyManager.ReturnIsHost() == false)
        {
            optionsButton.SetActive(false);
        }
    }
    public void DeactivateLobbyWindow()
    {
        lobbyWindow.gameObject.SetActive(false);
    }
    public void UpdatePlayersInLobby()
    {
        foreach (Transform child in currentPlayersContent)
        {
            Destroy(child.gameObject);
        }
        GameObject tempGO;
        UI_PlayerDataInLobby tempDATA;
        bool isHost = false;

        //Debug.Log("Host id == " + lobbyManager.currentLobby.HostId);
        //Debug.Log("CurrentPlayer id == " + lobbyManager.currentPlayer.Id);
        if (lobbyManager.currentLobby.HostId == lobbyManager.currentPlayer.Id)
        {
            isHost = true;
        }


        //Debug.Log("Players in lobby = " + lobbyData.Players.Count);
        foreach (Player player in lobbyManager.currentLobby.Players)
        {
            //Debug.Log("PLayer name = " + player.Data["PlayerName"].Value);
            //Debug.Log("PLayer id = " + player.Id);
            tempGO = Instantiate(playerDataPrefab);
            tempDATA = tempGO.GetComponent<UI_PlayerDataInLobby>();
            tempDATA.playerName.text = player.Data["PlayerName"].Value;
            tempDATA.playerID = player.Id;
            tempGO.transform.SetParent(currentPlayersContent);
            tempGO.transform.localScale = Vector3.one;
            

            //Debug.Log("Ready status = " + player.Data["Ready"].Value);
            if (player.Data["Ready"].Value == "0")
            {
                tempDATA.readyImage.color = redColor;
            }
            else
            {
                tempDATA.readyImage.color = greenColor;
            }
            

            if (isHost == false)
            {
                tempDATA.DisableKickButton();
            }
        }
        //Debug.LogWarning("Not finished");
    }

    public void BTN_Ready()
    {
        if(lobbyManager.ReturnIsHost() == false)
        {
            Debug.Log("Im not host");
            lobbyManager.CallMarkMeReady();
        }
        else
        {
            lobbyManager.CallMarkMeReady("1");
            bool readyCheck = true;
            foreach(Player player in lobbyManager.currentLobby.Players)
            {
                if (player.Data["Ready"].Value == "0")
                {
                    Debug.Log("Player  |  " + player.Data["PlayerName"].Value + "  |  is not Ready");
                    readyCheck = false;
                }
            }

            if(readyCheck == true)
            {
                ui_MainMenu.BTN_StartGame();
            }
        }
    }
    public void BTN_Leave()
    {
        lobbyManager.CallLeaveLobby();
        DeactivateLobbyWindow();
    }
    public void BTN_Show()
    {

    }
    public void BTN_Hide()
    {

    }
    public void BTN_ChooseCharacter()
    {

    }
    public void BTN_Options()
    {

    }
    public void CallKickPlayer(string playerId)
    {
        lobbyManager.CallKickPlayer(playerId);
    }

    /*
    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] UI_MainMenu ui_MainMenu;
    [SerializeField] GameObject playerDataPrefab;
    [SerializeField] Transform currentPlayersContent;
    [SerializeField] TMP_Text currentLobbyName;
    [SerializeField] TMP_InputField newNameField;

    [SerializeField] UI_Window lobbyWindow;
    [SerializeField] UI_Window changeNameWindow;


    public void UpdateLobbyWindow()
    {
        foreach (Transform child in currentPlayersContent)
        {
            Destroy(child.gameObject);
        }
        GameObject tempGO;
        UI_PlayerDataInLobby tempDATA;
        bool isHost = false;

        //Debug.Log("Host id == " + lobbyManager.currentLobby.HostId);
        //Debug.Log("CurrentPlayer id == " + lobbyManager.currentPlayer.Id);
        if (lobbyManager.currentLobby.HostId == lobbyManager.currentPlayer.Id)
        {
            isHost = true;
        }


        //Debug.Log("Players in lobby = " + lobbyData.Players.Count);
        foreach (Player player in lobbyManager.currentLobby.Players)
        {
            //Debug.Log("PLayer name = " + player.Data["PlayerName"].Value);
            tempGO = Instantiate(playerDataPrefab);
            tempDATA = tempGO.GetComponent<UI_PlayerDataInLobby>();
            tempDATA.playerName.text = player.Data["PlayerName"].Value;
            tempDATA.playerID = player.Id;
            tempGO.transform.SetParent(currentPlayersContent);
            tempGO.transform.localScale = Vector3.one;

            if(isHost == false)
            {
                tempDATA.DisableKickButton();
            }
        }
    }

    

    public void BTN_ChangeName()
    {
        newNameField.text = "";
        changeNameWindow.OpenWindow();
    }
    public void BTN_SaveNewName()
    {
        changeNameWindow.CloseWindow();
        lobbyManager.CallChangeName(newNameField.text);
    }
    public void BTN_LeaveLobby()
    {
        lobbyWindow.CloseWindow();
        lobbyManager.CallLeaveLobby();
        ui_MainMenu.ShowMenuWindow();
    }
    public void ShowLobbyWindow()
    {
        lobbyWindow.gameObject.SetActive(true);
        ui_MainMenu.HideJoiningWindows();
        UpdateLobbyWindow();
    }
    public void CallKickPlayer(string id)
    {
        lobbyManager.CallKickPlayer(id);
    }
    public void CloseLobbyWindow()
    {
        lobbyWindow.CloseWindow();
        ui_MainMenu.ShowMenuWindow();
    }
    */
}
