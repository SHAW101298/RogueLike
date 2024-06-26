using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using TMPro;
using UnityEngine.UI;
using System;

public class LobbyManager : MonoBehaviour
{
    public TMP_InputField lobbyNameField;
    public TMP_InputField maxPlayersField;
    public Toggle privateLobbyToggle;
    public TMP_InputField lobbyPasswordField;


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

    async void CreateLobby()
    {
        try
        {
            string lobbyName = lobbyNameField.text;
            int maxPlayers = Convert.ToInt32(maxPlayersField.text);

            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = privateLobbyToggle.isOn;
            options.Password = lobbyPasswordField.text;

            await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
            // Modyfikator obra¿eñ dla poziomu trudnoœci
            Debug.Log("Created Lobby! ");
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
    public void ResetInputData()
    {
        lobbyNameField.text = "";
        maxPlayersField.text = "";
        privateLobbyToggle.isOn = false;
        lobbyPasswordField.text = "";
    }
}
