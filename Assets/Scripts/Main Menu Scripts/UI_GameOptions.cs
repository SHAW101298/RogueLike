using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ENUM_DifficultySetting
{
    veryEasy,
    easy,
    medium,
    hard,
    AsGodWanted
}


public class UI_GameOptions : MonoBehaviour
{
    [Header("Referencje")]
    [SerializeField] UI_LobbyList lobbyList;
    [Header("UI Elements")]
    [SerializeField] TMP_Dropdown difficultyDropdown;

    [Header("Settings")]
    public ENUM_DifficultySetting difficultyLevel;


    public void BTN_Create()
    {
        difficultyLevel = (ENUM_DifficultySetting)difficultyDropdown.value;
        NetworkTypeController.Instance.HostGameAsRelay(lobbyList.createdLobbyData);
    }
    public void BTN_Return()
    {

    }
}
