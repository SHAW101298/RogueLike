using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_LootRewardType
{
    gold,
    health,
    blessing,
    gun
}
public class LootReward : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] ENUM_LootRewardType rewardType;

    public void SetRewardAmount(int val)
    {
        amount = val;
    }

    public void GiveReward(PlayerData player)
    {
        switch(rewardType)
        {
            case ENUM_LootRewardType.gold:
                GameData.Instance.ModifyPlayersGold((int)player.networkData.OwnerClientId,amount);
                break;
            case ENUM_LootRewardType.health:
                player.AddCurrentHealth(amount);
                break;
            case ENUM_LootRewardType.blessing:
                break;
            case ENUM_LootRewardType.gun:
                break;
            default:
                Debug.LogError("UNKNOWN REWARD TYPE");
                break;
        }
    }
}
