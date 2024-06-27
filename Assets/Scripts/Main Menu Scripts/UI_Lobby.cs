using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEditor.PackageManager.UI;

public class UI_Lobby : MonoBehaviour
{
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

        //Debug.Log("Players in lobby = " + lobbyData.Players.Count);
        foreach (Player player in lobbyManager.currentLobby.Players)
        {
            //Debug.Log("PLayer name = " + player.Data["PlayerName"].Value);
            tempGO = Instantiate(playerDataPrefab);
            tempDATA = tempGO.GetComponent<UI_PlayerDataInLobby>();
            tempDATA.playerName.text = player.Data["PlayerName"].Value;
            tempGO.transform.SetParent(currentPlayersContent);
            tempGO.transform.localScale = Vector3.one;
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
}
