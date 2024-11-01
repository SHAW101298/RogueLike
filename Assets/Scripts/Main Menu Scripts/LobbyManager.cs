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
using System.Threading.Tasks;
using Unity.Services.Relay;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using System.Security.Cryptography;
using Unity.Netcode;

public class LobbyManager : MonoBehaviour
{
    #region
    public static LobbyManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        
    }
    #endregion

    [Header("REF")]
    [SerializeField] RelayManager relayManager;
    [SerializeField] NetworkTypeController networkTypeController;
    [SerializeField] GameSetup gameSetup;
    public UI_MainMenu ui_MainMenu;
    public UI_Lobby ui_Lobby;
    public UI_LobbyList ui_LobbyList;
    
    [SerializeField] AudioListener oldListener;
    [SerializeField] public Lobby currentLobby;
    [SerializeField] bool inLobby;
    public Player currentPlayer;
    //public TMP_Text lobbyName;
    float heartBeatTimer;
    float poolUpdateTimer;
    bool inGame;
    float countdownTimer;
    async void Start()
    {
        if (oldListener == null)
        {
            oldListener = Camera.main.gameObject.GetComponent<AudioListener>();
        }

        //gameSetup = GameSetup.Instance;
        DontDestroyOnLoad(this);

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
            inLobby = true;
            if (currentLobby.HostId != currentPlayer.Id)
                return;

            heartBeatTimer += Time.deltaTime;
            if (heartBeatTimer >= 14)
            {
                heartBeatTimer = 0;
                await LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }
        else
        {
            inLobby = false;
        }
    }
    async void HandleLobbyPoolUpdate()
    {
        if (currentLobby == null)
            return;

        poolUpdateTimer += Time.deltaTime;

        if(inGame == true)
        {
            // Update lobby only once per 10 seconds
            if(poolUpdateTimer >= 10f)
            {
                poolUpdateTimer = 0;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
                currentLobby = lobby;
            }
        }
        else
        {
            if (poolUpdateTimer >= 1.1f)
            {
                poolUpdateTimer = 0;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
                currentLobby = lobby;
                //ui_Lobby.UpdatePlayersInLobby();

                // Lobby Updated with Relay Code
            }
        }
        

    }

    

    public void CallCreateLobby(string name, string players, bool isPrivate, string password)
    {
        CreateLobby(name, players, isPrivate, password);
    }
    public void CallCreateLobby(LobbyCreationData lobbyData)
    {
        CreateLobby(lobbyData.name, lobbyData.players, lobbyData.isPrivate, lobbyData.password);
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
    public async void CallMarkMeReady()
    {
        try
        {
            string status = currentPlayer.Data["Ready"].Value;
            Debug.Log("Player id = " + currentPlayer.Id);
            Debug.Log("status = " + status);
            if (status == "0")
            {
                status = "1";
            }
            else
            {
                status = "0";
            }
            UpdatePlayerOptions options = new UpdatePlayerOptions()
            {
                Data = new Dictionary<string, PlayerDataObject>
                {
                    ["Ready"] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, status)
                }
            };
            currentPlayer.Data["Ready"].Value = status;

            Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(currentLobby.Id, currentPlayer.Id, options);
            currentLobby = lobby;
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void CallMarkMeReady(string val)
    {
        try
        {
            UpdatePlayerOptions options = new UpdatePlayerOptions()
            {
                Data = new Dictionary<string, PlayerDataObject>
                {
                    ["Ready"] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, val)
                }
            };
            currentPlayer.Data["Ready"].Value = val;
            Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(currentLobby.Id, currentPlayer.Id, options);
            currentLobby = lobby;
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }
    public async void CallChangeName(string newName)
    {
        try
        {
            UpdatePlayerOptions options = new UpdatePlayerOptions()
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                ["PlayerName"] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, newName)
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
                UI_Lobby.instance.DeactivateLobbyWindow();
                UI_ErrorHandler.instance.ShowErrorMessage("You left as a host. Host migrated.");
            }
        }
        catch( LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }

    public async void CreateLobby(string lobbyName, string players, bool isPrivate, string password)
    {
        try
        {
            int maxPlayers = Convert.ToInt32(players);

            // Mark ready as Host
            currentPlayer = GetPlayer();

            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = isPrivate;
            options.Player = currentPlayer;

            if(password.Length > 0)
            {
                if (password.Length >= 4)
                {
                    options.Password = password;
                }
            }
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
            currentLobby = lobby;
            Debug.LogWarning("Dodac modyfikator obrazen dla poziomu trudnosci");
            // Modyfikator obra¿eñ dla poziomu trudnoœci
            Debug.Log("Created Lobby! " + currentLobby.Name + "  " +  currentLobby.LobbyCode);
            FirstLobbyUpdate();
            //ui_Lobby.ActivateLobbyWindow();
            //Debug.Log("Print keygamestart = " + currentLobby.Data["Key_Game_Start"].Value);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async Task<string> CreateLobby(LobbyCreationData lobbyData)
    {
        try
        {

            int maxPlayers = Convert.ToInt32(lobbyData.players);

            // Mark ready as Host
            currentPlayer = GetPlayer();
            //currentPlayer.Data["Ready"].Value = "1";

            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = lobbyData.isPrivate;
            options.Player = currentPlayer;

            if (lobbyData.password.Length > 0)
            {
                if (lobbyData.password.Length >= 4)
                {
                    options.Password = lobbyData.password;
                }
            }
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyData.name, maxPlayers, options);
            currentLobby = lobby;
            Debug.LogWarning("Dodac modyfikator obrazen dla poziomu trudnosci");
            Debug.Log("Created Lobby! " + currentLobby.Name + "  " + currentLobby.LobbyCode);
            FirstLobbyUpdate();
            //ui_Lobby.ActivateLobbyWindow();
            //Debug.Log("Print keygamestart = " + currentLobby.Data["Key_Game_Start"].Value);
            return "1";
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        return "0";
    }
    async void ListLobbies()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            Debug.Log("Found " + queryResponse.Results.Count + " lobbies.");
            ui_LobbyList.PrintAvailableLobbies(queryResponse);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
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
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code, options);
            currentLobby = lobby;
            Debug.Log("Joined Lobby with code " + code);
            FirstLobbyUpdate();
            //ui_Lobby.ActivateLobbyWindow();
        }
        catch (LobbyServiceException e)
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
            FirstLobbyUpdate();
            //ui_Lobby.ActivateLobbyWindow();
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
            Debug.Log("Joined Lobby with id " + lobbyId);
            FirstLobbyUpdate();
            //ui_Lobby.ActivateLobbyWindow();
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void ChangeLobbyOptions()
    {
        try
        {
            int number = RandomNumberGenerator.GetInt32(15);
            UpdateLobbyOptions options = new UpdateLobbyOptions();

            
            options.Data = new Dictionary<string, DataObject>()
            {
                ["TestValue"] = new DataObject(DataObject.VisibilityOptions.Member, number.ToString()),
            };
            Lobby lobby = await LobbyService.Instance.UpdateLobbyAsync(currentLobby.Id, options);
            currentLobby = lobby;
            Debug.Log("Changed Lobby Options to " + number);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    // =============================================================================================
    void OnPlayerSignIn()
    {
        Debug.Log("Player Signed in with id = " + AuthenticationService.Instance.PlayerId);
    }

    Player GetPlayer()
    {
        string randomName = NameRandomizer.Instance.GetRandomName();
        Player newPlayer = new Player(AuthenticationService.Instance.PlayerId)
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                ["PlayerName"] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, randomName),
                ["Ready"] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "0"),
                ["Character"] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "0")
                //{ "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "BaseName") }
            }
        };
        return newPlayer;
    }
    public bool ReturnIsHost()
    {
        if(currentLobby.HostId == AuthenticationService.Instance.PlayerId)
        {
            return true;
        }
        return false;
    }
    public async void UpdateLobbyWithRelayCode(string code)
    {
        try
        {
            UpdateLobbyOptions options = new UpdateLobbyOptions();

            options.Data = new Dictionary<string, DataObject>()
            {
                ["Key_Game_Start"] = new DataObject(DataObject.VisibilityOptions.Member, code),
            };
            Lobby lobby = await LobbyService.Instance.UpdateLobbyAsync(currentLobby.Id, options);
            currentLobby = lobby;
            // We already do that from PoolUpdate
            //GameSetup.Instance.BeginStartingGame();
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }
    public async void FirstLobbyUpdate()
    {
        Debug.Log("First lobby Update");
        Lobby lobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
        currentLobby = lobby;
    }
    public void DisableAudioListener()
    {
        oldListener.enabled = false;
    }
}
