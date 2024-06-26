using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading;

public class LobbyManager : MonoBehaviour
{
    [Header("CREATE LOBBY")]
    public TMP_InputField lobbyNameField;
    public TMP_InputField maxPlayersField;
    public Toggle privateLobbyToggle;
    public TMP_InputField lobbyPasswordField;
    [Header("JOIN LOBBY")]
    public TMP_InputField codeField;
    public TMP_InputField passwordField;
    [Header("Current Lobby")]
    Lobby currentLobby;
    public TMP_Text lobbyName;
    float timer;
    [Header("LOBBY LIST")]
    public Transform lobbiesContent;
    public GameObject lobbyDataPrefab;
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += OnPlayerSignIn;
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    private void Update()
    {
        HandleLobbyHeartBeat();
    }

    async void HandleLobbyHeartBeat()
    {
        if (currentLobby != null)
        {
            timer += Time.deltaTime;
            if (timer >= 14)
            {
                timer = 0;
                await LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }
    }
    public void CallCreateLobby()
    {
        CreateLobby();
    }
    public void CallJoinLobbyByCode()
    {
        JoinLobbyByCode();
    }
    public void CallJoinLobbyByID(string lobbyId)
    {
        JoinLobbyById(lobbyId);
    }
    public void CallListLobbies()
    {
        foreach(Transform child in lobbiesContent)
        {
            Destroy(child.gameObject);
        }
        ListLobbies();
    }

    async void CreateLobby()
    {
        try
        {
            string lobbyName = lobbyNameField.text;
            int maxPlayers = Convert.ToInt32(maxPlayersField.text);

            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = privateLobbyToggle.isOn;
            string password = lobbyPasswordField.text;
            if(password.Length > 0)
            {
                if (password.Length < 8)
                    return;

                options.Password = password;

            }

            currentLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
            // Modyfikator obra¿eñ dla poziomu trudnoœci
            Debug.Log("Created Lobby! " + currentLobby.Name + "  " +  currentLobby.LobbyCode);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    async void ListLobbies()
    {
        QueryResponse queryResponse =  await Lobbies.Instance.QueryLobbiesAsync();
        GameObject tempGO;
        UI_LobbyData tempData;
        Debug.Log("Found " + queryResponse.Results.Count + " lobbies.");
        foreach(Lobby lobby in queryResponse.Results)
        {
            tempGO = Instantiate(lobbyDataPrefab);
            tempData = tempGO.GetComponent<UI_LobbyData>();
            tempData.id = lobby.Id;
            tempData.lobbyName.text = lobby.Name;
            tempData.slots.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
            if(lobby.HasPassword == true)
            {
                tempData.passwordProtected.SetActive(true);
            }
            tempGO.transform.SetParent(lobbiesContent);
            tempGO.transform.localScale = Vector3.one;
        }
    }
    async void JoinLobbyByCode()
    {
        try
        {
            await Lobbies.Instance.JoinLobbyByCodeAsync(codeField.text);
            Debug.Log("Joined Lobby with code " + codeField.text);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    async void JoinLobbyById(string lobbyId)
    {
        await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);
        Debug.Log("Joined Lobby with id " + lobbyId);
    }


    void OnPlayerSignIn()
    {
        Debug.Log("Player Signed in with id = " + AuthenticationService.Instance.PlayerId);
        
    }
    public void ResetInputData()
    {
        lobbyNameField.text = "";
        maxPlayersField.text = "";
        privateLobbyToggle.isOn = false;
        lobbyPasswordField.text = "";
    }
}
