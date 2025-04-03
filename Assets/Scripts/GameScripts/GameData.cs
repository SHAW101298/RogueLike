using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameData : NetworkBehaviour
{
    #region
    public static GameData Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
#endregion

    public int currentFloor;
    public int playerGold;
    public int playerKills;
    public float playerDamage;

    public void ResetData()
    {
        currentFloor = 0;
        playerGold = 0;
        playerKills = 0;
        playerDamage = 0;
    }

    public void SetFloor(int floor)
    {
        currentFloor = floor;
    }

    public void ModifyPlayersGold(int gold)
    {
        playerGold += gold;
    }
    public void ModifyPlayersKills(int kills)
    {
        playerKills += kills;
    }
    public void ModifyPlayersDamage(float damage)
    {
        playerDamage += damage;
    }



    [ServerRpc(RequireOwnership = false)]
    public void GetPlayerCount_ServerRPC()
    {
        int count = NetworkManager.Singleton.ConnectedClientsList.Count;
        GetPlayerCount_ClientRPC(count);
    }
    
    [ClientRpc] 
    public void GetPlayerCount_ClientRPC(int count)
    {

    }

}
