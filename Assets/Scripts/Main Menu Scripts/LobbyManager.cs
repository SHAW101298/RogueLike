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

public class LobbyManager : MonoBehaviour
{
    [Header("CREATE LOBBY")]
    public TMP_InputField lobbyNameField;
    public TMP_InputField maxPlayersField;
    public Toggle privateLobbyToggle;
    public TMP_InputField lobbyPasswordField;
    [Header("JOIN LOBBY")]
    public TMP_InputField idField;
    public TMP_InputField codeField;
    public TMP_InputField passwordField;
    [Header("Current Lobby")]
    Lobby currentLobby;
    public TMP_Text lobbyName;
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += OnPlayerSignIn;
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public void CallCreateLobby()
    {
        CreateLobby();
    }
    public void CallJoinLobby()
    {
        JoinLobbyByCode();
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
        
    }
    async void JoinLobbyByCode()
    {
        await Lobbies.Instance.JoinLobbyByCodeAsync(codeField.text);
    }
    async void JoinLobbyById()
    {
        await Lobbies.Instance.JoinLobbyByIdAsync(idField.text);
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
