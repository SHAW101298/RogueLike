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
    [SerializeField] AudioListener oldListener;
    bool countdown;
    [SerializeField]float timer;
    [SerializeField] float timerMax = 1;

    // Start is called before the first frame update
    void Start()
    {
        lobbyManager = LobbyManager.Instance;
        relayManager = RelayManager.Instance;
    }
    private void Update()
    {
        //CheckForRelayCode();
        CountdownToSceneChange();

    }

    void CountdownToSceneChange()
    {
        if(countdown == true)
        {
            if (lobbyManager.ReturnIsHost() == false)
            {
                Debug.Log("Im not a host, so i wont start");
                return;
            }
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
    public void DisableAudioListener()
    {
        oldListener.enabled = false;
    }
    
    public void BeginCountDown()
    {
        Debug.Log("Beggining CountDown");
        countdown = true;
    }
}
