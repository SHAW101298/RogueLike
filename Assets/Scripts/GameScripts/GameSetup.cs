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

    [SerializeField] MapManager mapManager;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Setting Up a Game");
        GameData.Instance.SetNewPlayerAmount(NetworkManager.Singleton.ConnectedClientsIds.Count);
   
        CreateMap();
    }


    public void CreateMap()
    {
        // If Im host, i dont request for map
        if(NetworkManager.Singleton.IsHost == true)
        {
            mapManager.GenerateFloors();
            return;
        }
        mapManager.RequestMapLayout();
        //roomGenerator.RequestMapLayout_ServerRPC(NetworkManager.Singleton.LocalClientId);
    }
}
