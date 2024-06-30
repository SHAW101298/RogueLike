using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        lobbyManager = LobbyManager.Instance;
        relayManager = RelayManager.Instance;
    }


    public void LobbyDataChanged(Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>> data)
    {
        Debug.Log("Detected Data Change");
        // Relay code uploaded
        //Debug.Log("data = "+ data);
        //Debug.Log("values = " + data.Values);
        foreach( string info in data.Keys)
        {
            Debug.Log("Present key = " + info);
        }
        foreach (ChangedOrRemovedLobbyValue<DataObject> info in data.Values)
        {
            Debug.Log("Present value = " + info.Value.Value);
        }
        /*
        if (data["Key_Game_Start"].Value.Value != "0")
        {
            // Im not a host
            if(lobbyManager.ReturnIsHost() == false)
            {
                relayManager.JoinRelay(lobbyManager.currentLobby.Data["Key_Game_Start"].Value);
            }
            lobbyManager.currentLobby = null; // Destroyes current lobby after 30 seconds
            ui_MainMenu.HideLobbyWindow();
            ui_MainMenu.ShowMenuWindow();
        }
        */


        //if (currentLobby.Data["Key_Game_Start"].Value != "0")

    }
}
