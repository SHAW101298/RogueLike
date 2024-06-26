using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] 

    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += OnPlayerSignIn;
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    async void CreateLobby()
    {
        CreateLobbyOptions options = new CreateLobbyOptions();
        string lobbyName = "New Lobby";
        await LobbyService.Instance.CreateLobbyAsync(lobbyName, 4,options);
    }



    void OnPlayerSignIn()
    {
        Debug.Log("Player Signed in with id = " + AuthenticationService.Instance.PlayerId);
    }
}
