using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEditor.PackageManager.UI;

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
}
