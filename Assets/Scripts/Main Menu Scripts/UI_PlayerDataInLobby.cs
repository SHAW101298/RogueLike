using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_PlayerDataInLobby : MonoBehaviour
{
    private void Start()
    {
        
    }
    public TMP_Text playerName;
    public GameObject kickButton;
    public Image characterImage;
    public Image readyImage;
    public string playerID;
    public void DisableKickButton()
    {
        kickButton.SetActive(false);
    }
    public void BTN_KickPlayer()
    {
        UI_Lobby.instance.CallKickPlayer(playerID);
    }
}
