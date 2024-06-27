using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_CreateLobby : MonoBehaviour
{
    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] TMP_InputField lobbyNameField;
    [SerializeField] TMP_InputField maxPlayersField;
    [SerializeField] Toggle privateLobbyToggle;
    [SerializeField] TMP_InputField lobbyPasswordField;
    [SerializeField] UI_Window createLobbyWindow;
    public void BTN_Create()
    {
        lobbyManager.CallCreateLobby(lobbyNameField.text, maxPlayersField.text, privateLobbyToggle.isOn, lobbyPasswordField.text);
    }
    public void BTN_Return()
    {
        createLobbyWindow.CloseWindow();
        createLobbyWindow.parent.OpenWindow();
        ResetInputData();
    }
    void ResetInputData()
    {
        lobbyNameField.text = "";
        maxPlayersField.text = "";
        privateLobbyToggle.isOn = false;
        lobbyPasswordField.text = "";
    }
}
