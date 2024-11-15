using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerList : MonoBehaviour
{
    #region // Instance Creation
    public static PlayerList Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }    
    }
    #endregion 
    public string sceneName;
    public List<PlayerData> players;

    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        sceneName = currentSceneName;
        if(currentSceneName.Equals("GameScene") || currentSceneName.Equals("SampleScene"))
        {
            AddPlayers();
        }
    }
    

    public void RegisterToEvents()
    {
        NetworkManager.Singleton.SceneManager.OnLoadComplete += SceneLoadCompleted;
        NetworkManager.Singleton.OnClientDisconnectCallback += PlayerDisconected;
    }
    public void UnregisterFromEvents()
    {
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= SceneLoadCompleted;
        NetworkManager.Singleton.OnClientDisconnectCallback -= PlayerDisconected;
    }

    private void PlayerDisconected(ulong obj)
    {
        players.Clear();
        AddPlayers();
    }

    private void SceneLoadCompleted(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        if(sceneName.Equals("GameScene") || sceneName.Equals("SampleScene"))
        {
            Debug.Log("Scene Loaded");
            AddPlayers();
        }
    }

    private void AddPlayers()
    {
        NetworkClient client;
        int playerCount = NetworkManager.Singleton.ConnectedClients.Count;
        for (int i = 0; i < playerCount; i++)
        {
            client = NetworkManager.Singleton.ConnectedClientsList[i];
            AddPlayerToList(client.PlayerObject.gameObject.GetComponent<PlayerData>());
        }
    }

    



    void AddPlayerToList(PlayerData player)
    {
        players.Add(player);
    }
    void RemovePlayerFromList(PlayerData player)
    {
        players.Remove(player);
    }
    public PlayerData GetPlayer(int id)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if (players[i].networkData.OwnerClientId == (ulong)id)
            {
                return players[i];
            }
        }
        return null;
    }
    public PlayerData GetClosestPlayer(Vector3 pos)
    {
        float closestDistance = Vector3.Distance(pos, players[0].transform.position);
        PlayerData closestPlayer = players[0];

        for(int i = 1; i < players.Count; i++)
        {
            float dist = Vector3.Distance(pos, players[i].transform.position);
            if(dist < closestDistance)
            {
                closestDistance = dist;
                closestPlayer = players[i];
            }
        }

        return closestPlayer;
    }
    public float GetDistanceToClosestPlayer(Vector3 pos)
    {
        float closestDistance = Vector3.Distance(pos, players[0].transform.position);
        //PlayerData closestPlayer = players[0];

        for (int i = 1; i < players.Count; i++)
        {
            float dist = Vector3.Distance(pos, players[i].transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                //closestPlayer = players[i];
            }
        }

        return closestDistance;
    }
    
    public PlayerData GetMyPlayer()
    {
        PlayerData player = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject.GetComponent<PlayerData>();
        return player;
    }

}
