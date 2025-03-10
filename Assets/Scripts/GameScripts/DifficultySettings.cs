using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    public ENUM_DifficultySetting setting;
    public float aimCorrection;
    public float goldMultiplier;
    public float enemyHealthMultiplier;
    public float enemyShieldMultiplier;
    public float enemyDamageMultiplier;
    public float playerStaminaMultiplier;
    public float playerStaminaRegenMultiplier;
    public ElementalTable enemyFlatResistanceIncrease;
    public ElementalTable enemyPercentResistanceIncrease;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    public void Apply()
    {
        Debug.Log("Applying difficulty setting " + setting.ToString());
        Debug.LogError("CHANGED WAYS. APPLY ON ENEMIES SPAWN");
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
        enemy.stats.flatResistance.AddData(enemyFlatResistanceIncrease);
        enemy.stats.percentResistance.AddData(enemyPercentResistanceIncrease);
    }

}
