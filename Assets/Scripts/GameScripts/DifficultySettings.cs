using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    public ENUM_DifficultySetting setting;
    public float aimCorrection;
    public float goldMultiplier;
    [Space(10)]
    public float enemyHealthMultiplier;
    public float enemyShieldMultiplier;
    public float enemyDamageMultiplier;
    public float playerStaminaMultiplier;
    public float playerStaminaRegenMultiplier;
    public float playerHealthMultiplier;
    public float playerHealthRegenMultiplier;
    public float playerShieldMultiplier;
    public float playerShieldRegenMultiplier;
    [Space(10)]
    public float gunDropChance;
    public float blessingDropChance;
    [Space(10)]
    public ElementalTable enemyFlatResistanceIncrease;
    public ElementalTable enemyPercentResistanceIncrease;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    public void Apply()
    {
        Debug.Log("Applying difficulty setting " + setting.ToString());
        //Debug.LogError("CHANGED WAYS. APPLY ON ENEMIES SPAWN");
        /*
        foreach(RoomManager room in RoomGenerator.Instance.currentRooms)
        {
            foreach(EnemyData enemy in room.enemiesInRoom)
            {
                enemy.stats.healthMax *= enemyHealthMultiplier;
                enemy.stats.health *= enemyHealthMultiplier;
                enemy.stats.shieldMax *= enemyShieldMultiplier;
                enemy.stats.shield *= enemyShieldMultiplier;
                enemy.stats.flatResistance.AddData(enemyFlatResistanceIncrease);
                enemy.stats.percentResistance.AddData(enemyPercentResistanceIncrease);
            }
        }
        */
    }
    public void Apply(EnemyData enemy)
    {
        enemy.stats.healthMax *= enemyHealthMultiplier;
        enemy.stats.health *= enemyHealthMultiplier;
        enemy.stats.shieldMax *= enemyShieldMultiplier;
        enemy.stats.shield *= enemyShieldMultiplier;
        enemy.bonusStats.flatElementalResistance.AddData(enemyFlatResistanceIncrease);
        enemy.bonusStats.percentElementalResistance.AddData(enemyPercentResistanceIncrease);
    }
    public void ApplyForPlayer(PlayerData player)
    {
        player.baseStats.healthMax *= playerHealthMultiplier;
        player.baseStats.healthRegen *= playerHealthRegenMultiplier;
        player.baseStats.staminaMax *= playerStaminaMultiplier;
        player.baseStats.staminaRegen *= playerStaminaRegenMultiplier;
        player.baseStats.shieldMax *= playerShieldMultiplier;
        player.baseStats.shieldRegen *= playerShieldRegenMultiplier;
    }

}
