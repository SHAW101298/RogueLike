using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
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
    public List<int> playerGold;
    public List<int> playerKills;
    public List<float> playerDamage;

    public void ResetData()
    {
        currentFloor = 0;
        playerGold.Clear();
        playerKills.Clear();
        playerDamage.Clear();
    }
    public void SetNewPlayerAmount(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            playerGold.Add(0);
            playerKills.Add(0);
            playerDamage.Add(0);
        }
    }
    public void SetFloor(int floor)
    {
        currentFloor = floor;
    }

    public void ModifyPlayersGold(int player, int gold)
    {
        if (player > playerGold.Count)
        {
            Debug.LogError("Trying to change gold data of nonexistent Player");
            return;
        }
        playerGold[player] += gold;
    }
    public void ModifyPlayersKills(int player, int kills)
    {
        if (player > playerKills.Count)
        {
            Debug.LogError("Trying to change kills data of nonexistent Player");
            return;
        }
        playerKills[player] += kills;
    }
    public void ModifyPlayersDamage(int player, float damage)
    {
        if (player > playerDamage.Count)
        {
            Debug.LogError("Trying to change damage data of nonexistent Player");
            return;
        }
        playerDamage[player] += damage;
    }
}
