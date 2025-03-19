using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public LootReward rewardPrefab;
    public int min;
    public int max;
    public float chance;

    public bool CheckIfGotten()
    {
        int x = Random.Range(0, 100);
        if(x <= chance)
        {
            return true;
        }
        return false;
    }
    public int CalculateReward()
    {
        return Random.Range(min, max);
    }
}
public class BreakAbles : MonoBehaviour
{
    public float health;
    public Loot[] LootTable;
    public GameObject brokenReplacement;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            CheckLoot();
            CreateBrokenObject();
        }
    }
    void CheckLoot()
    {
        foreach (Loot loot in LootTable)
        {
            // Check if Loot was obtained
            if(loot.CheckIfGotten() == true)
            {
                GameObject temp = Instantiate(loot.rewardPrefab.gameObject);
                // Slightly Displace Loot
                Vector3 lootPos = transform.position;
                lootPos.x += Random.Range(0, 0.5f);
                lootPos.z += Random.Range(0, 0.5f);
                temp.transform.position = lootPos;
                // Calculate Amount of Loot
                LootReward tempReward= temp.GetComponent<LootReward>();
                tempReward.SetRewardAmount(loot.CalculateReward());
            }
        }
    }
    void CreateBrokenObject()
    {
        GameObject temp = Instantiate(brokenReplacement);
        temp.transform.position = transform.position;
        Destroy(gameObject);
    }

}
