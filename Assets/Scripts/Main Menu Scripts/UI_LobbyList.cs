using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;

public class UI_LobbyList : MonoBehaviour
{
    #region
    public static UI_LobbyList instance;
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
    
    [SerializeField] Transform lobbyListContent;
    [SerializeField] GameObject lobbyPrefab;
    [SerializeField] UI_Window joinLobbyFromListWindow;
    [Space(15)]
    [SerializeField] GameObject passwordRequest;
    [SerializeField] TMP_InputField passwordField;
    

    public string selectedLobby = "-1";

    public void SelectLobby(string id)
    {
        selectedLobby = id;
    }

    public void PrintAvailableLobbies(QueryResponse queryResponse)
    {
        // Destroy Children
        foreach (Transform child in lobbyListContent)
        {
            Destroy(child.gameObject);
        }
        GameObject tempGO;
        UI_LobbyData tempData;
        foreach (Lobby lobby in queryResponse.Results)
        {
            tempGO = Instantiate(lobbyPrefab);
            tempData = tempGO.GetComponent<UI_LobbyData>();
            tempData.id = lobby.Id;
            tempData.lobbyName.text = lobby.Name;
            tempData.slots.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
            if (lobby.HasPassword == true)
            {
                tempData.passwordProtected.SetActive(true);
            }
            tempGO.transform.SetParent(lobbyListContent);
            tempGO.transform.localScale = Vector3.one;
        }
    }

    public void BTN_Refresh()
    {
        lobbyManager.CallListLobbies();
        selectedLobby = "-1";
    }
    public async void BTN_Join()
    {
        if (selectedLobby == "-1")
            return;

        Lobby lobby = await LobbyService.Instance.GetLobbyAsync(selectedLobby);
        if(lobby.HasPassword == true)
        {
            passwordRequest.SetActive(true);
            return;
        }
        lobbyManager.CallJoinLobbyByID(selectedLobby);
    }
    public void BTN_JoinWithPassword()
    {
        lobbyManager.CallJoinLobbyByID(selectedLobby, passwordField.text);
    }
    public void BTN_Return()
    {
        joinLobbyFromListWindow.CloseWindow();
        joinLobbyFromListWindow.parent.OpenWindow();
    }
}
