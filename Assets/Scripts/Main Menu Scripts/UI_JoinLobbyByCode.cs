using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_JoinLobbyByCode : MonoBehaviour
{
    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] TMP_InputField codeField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] UI_Window joinLobbyByCodeWindow;


    public void BTN_Join()
    {
        lobbyManager.CallJoinLobbyByCode(codeField.text, passwordField.text);
    }
    public void BTN_Return()
    {
        joinLobbyByCodeWindow.CloseWindow();
        joinLobbyByCodeWindow.parent.OpenWindow();
    }
}
