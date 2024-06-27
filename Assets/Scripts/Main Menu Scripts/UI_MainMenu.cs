using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;



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
    public Transform lobbiesContent;
    public GameObject lobbyDataPrefab;
    public GameObject playerDataPrefab;
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
    [Header("Create Lobby Fields")]
    [SerializeField] TMP_InputField ClobbyNameField;
    [SerializeField] TMP_InputField CmaxPlayersField;
    [SerializeField] Toggle CprivateLobbyToggle;
    [SerializeField] TMP_InputField ClobbyPasswordField;
    [Header("Join Lobby Fields")]
    [SerializeField] TMP_InputField JcodeField;
    [SerializeField] TMP_InputField JpasswordField;
    [Header("Current Lobby Fields")]
    [SerializeField] Transform currentPlayersContent;
    [SerializeField] TMP_Text currentLobbyName;
    
    [Header("Other Fields")]
    [SerializeField] TMP_InputField newNameField;



    public string selectedLobby = "-1";

    public void SelectLobby(string id)
    {
        selectedLobby = id;
    }

    public void PrintAvailableLobbies(QueryResponse queryResponse)
    {
        // Destroy Children
        foreach(Transform child in lobbiesContent)
        {
            Destroy(child.gameObject);
        }
        GameObject tempGO;
        UI_LobbyData tempData;
        foreach (Lobby lobby in queryResponse.Results)
        {
            tempGO = Instantiate(lobbyDataPrefab);
            tempData = tempGO.GetComponent<UI_LobbyData>();
            tempData.id = lobby.Id;
            tempData.lobbyName.text = lobby.Name;
            tempData.slots.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
            if (lobby.HasPassword == true)
            {
                tempData.passwordProtected.SetActive(true);
            }
            tempGO.transform.SetParent(lobbiesContent);
            tempGO.transform.localScale = Vector3.one;
        }
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
        lobbyManager.CallCreateLobby(ClobbyNameField.text, CmaxPlayersField.text, CprivateLobbyToggle.isOn, ClobbyPasswordField.text);
    }
    public void BTN_CreateLobbyReturn()
    {
        createLobbyWindow.CloseWindow();
        createLobbyWindow.parent.OpenWindow();
        ResetInputData();
    }
    void ResetInputData()
    {
        ClobbyNameField.text = "";
        CmaxPlayersField.text = "";
        CprivateLobbyToggle.isOn = false;
        ClobbyPasswordField.text = "";
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
    public void BTN_JoinLobbyFromList()
    {
        joinLobbyFromListWindow.OpenWindow();
        joinLobbyFromListWindow.parent.CloseWindow();
    }
    public void BTN_JoinLobbyFromListRefresh()
    {
        lobbyManager.CallListLobbies();
        selectedLobby = "-1";
    }
    public void BTN_JoinLobbyFromListJOIN()
    {
        if (selectedLobby == "-1")
            return;
        lobbyManager.CallJoinLobbyByID(selectedLobby);

    }
    public void BTN_JoinLobbyFromListReturn()
    {
        joinLobbyFromListWindow.CloseWindow();
        joinLobbyFromListWindow.parent.OpenWindow();
    }
    // ====================================
    
    public void BTN_JoinLobbyByCode()
    {
        joinLobbyByCodeWindow.OpenWindow();
        joinLobbyByCodeWindow.parent.CloseWindow();
    }
    public void BTN_JoinLobbyByCodeJOIN()
    {
        lobbyManager.CallJoinLobbyByCode(JcodeField.text, JpasswordField.text);
    }
    public void BTN_JoinLobbyByCodeReturn()
    {
        joinLobbyByCodeWindow.CloseWindow();
        joinLobbyByCodeWindow.parent.OpenWindow();
    }
    // ====================================

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
    }
    public void ShowLobbyWindow(Lobby lobbyData)
    {
        createLobbyWindow.CloseWindow();
        joinLobbyByCodeWindow.CloseWindow();
        joinLobbyFromListWindow.CloseWindow();
        lobbyWindow.OpenWindow();
        foreach (Transform child in currentPlayersContent)
        {
            Destroy(child.gameObject);
        }
        GameObject tempGO;
        UI_PlayerDataInLobby tempDATA;

        Debug.Log("Players in lobby = " + lobbyData.Players.Count);
        foreach(Player player in lobbyData.Players)
        {
            tempGO = Instantiate(playerDataPrefab);
            tempDATA = tempGO.GetComponent<UI_PlayerDataInLobby>();
            tempDATA.playerName.text = player.Data["PlayerName"].Value;
            tempGO.transform.SetParent(currentPlayersContent);
            tempGO.transform.localScale = Vector3.one;
        }
    }
    public void UpdateLobbyWindow(Lobby lobbyData)
    {
        foreach (Transform child in currentPlayersContent)
        {
            Destroy(child.gameObject);
        }
        GameObject tempGO;
        UI_PlayerDataInLobby tempDATA;

        //Debug.Log("Players in lobby = " + lobbyData.Players.Count);
        foreach (Player player in lobbyData.Players)
        {
            Debug.Log("PLayer name = " + player.Data["PlayerName"].Value);
            tempGO = Instantiate(playerDataPrefab);
            tempDATA = tempGO.GetComponent<UI_PlayerDataInLobby>();
            tempDATA.playerName.text = player.Data["PlayerName"].Value;
            tempGO.transform.SetParent(currentPlayersContent);
            tempGO.transform.localScale = Vector3.one;
        }
    }

}
