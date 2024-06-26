using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_LobbyData : MonoBehaviour
{
    public string id;
    public TMP_Text lobbyName;
    public TMP_Text slots;
    public GameObject passwordProtected;
    public void ClickedOnLobby()
    {
        UI_MainMenu.instance.SelectLobby(id);
    }
}
