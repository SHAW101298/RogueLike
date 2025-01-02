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
    [SerializeField] GameOptions gameOptions;
    [Header("UI Elements")]
    [SerializeField] TMP_Dropdown difficultyDropdownMultiPlayer;
    [SerializeField] TMP_Dropdown difficultyDropdownSinglePlayer;

    [Header("Settings")]
    public ENUM_DifficultySetting difficultyLevel;


    private void Start()
    {
        gameOptions = GameOptions.Instance;
    }

    public void BTN_Create()
    {
        gameOptions.difficultyLevel = (ENUM_DifficultySetting)difficultyDropdownMultiPlayer.value;
        Debug.Log(" Difficulty Level Read as " + (ENUM_DifficultySetting)difficultyDropdownMultiPlayer.value);
        NetworkTypeController.Instance.HostGameAsRelay(lobbyList.createdLobbyData);
    }
    public void BTN_ReturnSinglePlayer()
    {
        UI_MainMenu.instance.HideGameOptionsWindow();
    }
    public void BTN_ReturnMultiPlayer()
    {
        UI_MainMenu.instance.HideGameOptionsWindow();

    }
    public void ReadGameOptions()
    {
        gameOptions.difficultyLevel = (ENUM_DifficultySetting)difficultyDropdownSinglePlayer.value;
    }
}
