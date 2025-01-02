using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine.UI;

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
    [Header("References")]
    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] UI_MainMenu ui_MainMenu;
    [Header("Windows")]
    [SerializeField] GameObject lobbyListWindow;
    [SerializeField] GameObject passwordRequestWindow;
    [SerializeField] GameObject codeInputWindow;
    [SerializeField] GameObject createLobbyWindow;
    [Header("Other")]
    [SerializeField] Transform lobbyListContent;
    [SerializeField] GameObject lobbyPrefab;
    [SerializeField] UI_Window joinLobbyFromListWindow;
    [Space(15)]
    //[SerializeField] GameObject passwordRequest;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] TMP_InputField codeField;
    [Header("Creating Lobby")]
    [SerializeField] TMP_InputField newLobbyName;
    [SerializeField] TMP_InputField newLobbyPlayers;
    [SerializeField] Toggle newLobbyPrivate;
    [SerializeField] TMP_InputField newLobbyPassword;

    public LobbyCreationData createdLobbyData { get; private set; }
    

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
            passwordRequestWindow.SetActive(true);
            return;
        }
        NetworkTypeController.Instance.JoinGameOnRelayByID(lobby.Id);
        //lobbyManager.CallJoinLobbyByID(selectedLobby);
    }
    public void BTN_JoinWithPassword()
    {
        string savedPassword = passwordField.text;
        passwordField.text = "";
        NetworkTypeController.Instance.JoinGameOnRelayByID(selectedLobby, savedPassword);
        //lobbyManager.CallJoinLobbyByID(selectedLobby, savedPassword);
    }
    public void BTN_Return()
    {
        joinLobbyFromListWindow.CloseWindow();
        joinLobbyFromListWindow.parent.OpenWindow();
    }
    public void BTN_OpenCreateNewLobby()
    {
        createLobbyWindow.SetActive(true);
        lobbyListWindow.SetActive(false);
    }
    public void BTN_ExitCreateNewLobby()
    {
        createLobbyWindow.SetActive(false);
        lobbyListWindow.SetActive(true);
    }
    public void BTN_CreateNewLobby()
    {
        //string lobbyName = newLobbyName.text;
        //string players = newLobbyPlayers.text;
        //bool isPrivate = newLobbyPrivate.isOn;
        //string password = newLobbyPassword.text;
        LobbyCreationData newLobbyData = new LobbyCreationData();
        newLobbyData.name = newLobbyName.text;
        newLobbyData.players = newLobbyPlayers.text;
        newLobbyData.isPrivate = newLobbyPrivate.isOn;
        newLobbyData.password = newLobbyPassword.text;

        createLobbyWindow.SetActive(false);
        lobbyListWindow.SetActive(true);
        ui_MainMenu.BTN_MultiplayerReturn();
        ResetCreateLobbyData();
        //LoadingInfo.Instance.UpdateCurrentProgress("Creating Lobby");
        //lobbyManager.CallCreateLobby(newLobbyName.text, newLobbyPlayers.text, newLobbyPrivate.isOn, newLobbyPassword.text);
        //NetworkTypeController.Instance.HostGameAsRelay(newLobbyData);
        //lobbyManager.CallCreateLobby(lobbyName, players, isPrivate, password);

        createdLobbyData = newLobbyData;
        ui_MainMenu.BTN_ShowGameOptionsWindow_Multiplayer();
    }
    void ResetCreateLobbyData()
    {
        newLobbyName.text = "";
        newLobbyPlayers.text = "";
        newLobbyPrivate.isOn = false;
        newLobbyPassword.text = "";
    }
    public void BTN_OpenJoinWithCode()
    {
        lobbyListWindow.SetActive(false);
        codeInputWindow.SetActive(true);
    }
    public void BTN_ExitJoinWithCode()
    {
        lobbyListWindow.SetActive(true);
        codeInputWindow.SetActive(false);
    }
    public void BTN_JoinWithCode()
    {
        string code = codeField.text;
        BTN_ExitJoinWithCode();
        NetworkTypeController.Instance.JoinGameOnRelayByCode(code);
        //lobbyManager.CallJoinLobbyByCode(code);
    }
    public void BTN_ExitPasswordInput()
    {
        passwordRequestWindow.SetActive(false);
    }
}
