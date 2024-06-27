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
    [Header("REF")]
    public UI_MainMenu ui_MainMenu;
    [Header("Current Lobby")]
    Lobby currentLobby;
    Player currentPlayer;
    //public TMP_Text lobbyName;
    float heartBeatTimer;
    float poolUpdateTimer;
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += OnPlayerSignIn;

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    private void Update()
    {
        HandleLobbyHeartBeat();
        HandleLobbyPoolUpdate();
    }

    async void HandleLobbyHeartBeat()
    {
        if (currentLobby != null)
        {
            heartBeatTimer += Time.deltaTime;
            if (heartBeatTimer >= 14)
            {
                heartBeatTimer = 0;
                await LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }
    }
    async void HandleLobbyPoolUpdate()
    {
        if (currentLobby == null)
            return;

        poolUpdateTimer += Time.deltaTime;
        if(poolUpdateTimer >= 1.1f)
        {
            poolUpdateTimer = 0;
            Lobby lobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
            currentLobby = lobby;
            ui_MainMenu.UpdateLobbyWindow(lobby);
        }

    }
    public void CallCreateLobby(string name, string players, bool isPrivate, string password)
    {
        CreateLobby(name, players, isPrivate, password);
    }
    public void CallJoinLobbyByCode(string code, string password)
    {
        JoinLobbyByCode(code, password);
    }
    public void CallJoinLobbyByID(string lobbyId)
    {
        JoinLobbyById(lobbyId);
    }
    public void CallListLobbies()
    {
        ListLobbies();
    }
    public async void CallChangeName(string newName)
    {
        UpdatePlayerOptions options = new UpdatePlayerOptions()
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, newName) }
            }
        };
        Debug.Log("Current player id is " + currentPlayer.Id);
        Debug.Log("New Name is " + options.Data["PlayerName"].Value);
        Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(currentLobby.Id, currentPlayer.Id, options);
        currentLobby = lobby;
    }
    public async void CallLeaveLobby()
    {
        await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, currentPlayer.Id);
        Debug.Log("Left Lobby " + currentLobby.Name + " | " + currentLobby.Id);
        currentLobby = null;
    }

    async void CreateLobby(string lobbyName, string players, bool isPrivate, string password)
    {
        try
        {
            int maxPlayers = Convert.ToInt32(players);

            currentPlayer = GetPlayer();

            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = isPrivate;
            options.Player = currentPlayer;
            if(password.Length > 0)
            {
                if (password.Length < 8)
                    return;

                options.Password = password;

            }

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
            currentLobby = lobby;
            // Modyfikator obra¿eñ dla poziomu trudnoœci
            Debug.Log("Created Lobby! " + currentLobby.Name + "  " +  currentLobby.LobbyCode);
            ui_MainMenu.ShowLobbyWindow(lobby);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    async void ListLobbies()
    {
        QueryResponse queryResponse =  await Lobbies.Instance.QueryLobbiesAsync();
        Debug.Log("Found " + queryResponse.Results.Count + " lobbies.");     
        ui_MainMenu.PrintAvailableLobbies(queryResponse);
    }
    async void JoinLobbyByCode(string code, string password)
    {
        try
        {
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code);
            currentLobby = lobby;
            Debug.Log("Joined Lobby with code " + code);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    async void JoinLobbyById(string lobbyId)
    {
        Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);
        currentLobby = lobby;
        Debug.Log("Joined Lobby with id " + lobbyId);
        
    }


    void OnPlayerSignIn()
    {
        Debug.Log("Player Signed in with id = " + AuthenticationService.Instance.PlayerId);
    }

    Player GetPlayer()
    {
        Player newPlayer = new Player(AuthenticationService.Instance.PlayerId)
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "BaseName") }
            }
        };
        return newPlayer;
    }
}
