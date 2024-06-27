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
    public UI_Lobby ui_Lobby;
    public UI_LobbyList ui_LobbyList;
    [Header("Current Lobby")]
    public Lobby currentLobby;
    public Player currentPlayer;
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
            ui_Lobby.UpdateLobbyWindow();
        }

    }
    public void CallCreateLobby(string name, string players, bool isPrivate, string password)
    {
        CreateLobby(name, players, isPrivate, password);
    }
    public void CallJoinLobbyByCode(string code)
    {
        JoinLobbyByCode(code);
    }
    public void CallJoinLobbyByID(string lobbyId)
    {
        JoinLobbyById(lobbyId);
    }
    public void CallJoinLobbyByID(string lobbyiD, string password)
    {
        JoinLobbyById(lobbyiD, password);
    }
    public void CallListLobbies()
    {
        ListLobbies();
    }
    public async void CallChangeName(string newName)
    {
        try
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
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }
    public async void CallLeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, currentPlayer.Id);
            Debug.Log("Left Lobby " + currentLobby.Name + " | " + currentLobby.Id);
            currentLobby = null;
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }
    public async void CallKickPlayer(string id)
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, id);
            Debug.Log("Kicking player " + id);
            if (id == currentPlayer.Id)
            {
                currentLobby = null;
                UI_Lobby.instance.CloseLobbyWindow();
                UI_ErrorHandler.instance.ShowErrorMessage("You left as a host. Host migrated.");
            }
        }
        catch( LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
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
            ui_Lobby.ShowLobbyWindow();
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
        ui_LobbyList.PrintAvailableLobbies(queryResponse);
    }
    async void JoinLobbyByCode(string code)
    {
        try
        {
            currentPlayer = GetPlayer();
            JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions
            {
                Player = currentPlayer
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code,options);
            currentLobby = lobby;
            Debug.Log("Joined Lobby with code " + code);
            ui_Lobby.ShowLobbyWindow();
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    async void JoinLobbyById(string lobbyId)
    {
        try
        {
            currentPlayer = GetPlayer();
            JoinLobbyByIdOptions options = new JoinLobbyByIdOptions()
            {
                Player = currentPlayer
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId, options);
            currentLobby = lobby;
            Debug.Log("Joined Lobby with id " + lobbyId);
            ui_Lobby.ShowLobbyWindow();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }
    async void JoinLobbyById(string lobbyId, string password)
    {
        try
        {
            currentPlayer = GetPlayer();
            JoinLobbyByIdOptions options = new JoinLobbyByIdOptions()
            {
                Password = password,
                Player = currentPlayer
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId, options);
            currentLobby = lobby;
            ui_Lobby.ShowLobbyWindow();
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
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
