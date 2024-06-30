using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    #region
    public static GameSetup Instance;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] LobbyManager lobbyManager;
    [SerializeField] RelayManager relayManager;
    [SerializeField] UI_MainMenu ui_MainMenu;
    bool countdown;
    [SerializeField]float timer;
    [SerializeField] float timerMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        lobbyManager = LobbyManager.Instance;
        relayManager = RelayManager.Instance;
    }

    void CountdownToSceneChange()
    {
        if(countdown == true)
        {
            timer += Time.deltaTime;
            if(timer >= timerMax)
            {
                Debug.Log("Loading New Scene");
                timer = 0;
                NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
                countdown = false;
            }
        }
    }

    void CheckForRelayCode()
    {
        if (LobbyManager.Instance.currentLobby.Data["Key_Game_Start"].Value != "0")
        {
            // Im not a host
            if (lobbyManager.ReturnIsHost() == false)
            {
                relayManager.JoinRelay(lobbyManager.currentLobby.Data["Key_Game_Start"].Value);
            }
            Debug.Log("Proceeding with Game Start");
            lobbyManager.currentLobby = null; // Destroyes current lobby after 30 seconds
            ui_MainMenu.HideLobbyWindow();
            ui_MainMenu.ShowMenuWindow();
            countdown = true;
        }
    }
    private void Update()
    {
        CheckForRelayCode();
        CountdownToSceneChange();

    }

    public void BeginStartingGame()
    {
        if (LobbyManager.Instance.currentLobby.Data["Key_Game_Start"].Value != "0")
        {
            // Im not a host
            if (lobbyManager.ReturnIsHost() == false)
            {
                relayManager.JoinRelay(lobbyManager.currentLobby.Data["Key_Game_Start"].Value);
            }
            Debug.Log("Proceeding with Game Start");
            lobbyManager.currentLobby = null; // Destroyes current lobby after 30 seconds
            ui_MainMenu.HideLobbyWindow();
            ui_MainMenu.ShowMenuWindow();
            countdown = true;
        }
    }
}
