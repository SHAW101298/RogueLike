using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptions : MonoBehaviour
{
    // Instance
    #region
    public static GameOptions Instance;

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


    public ENUM_DifficultySetting difficultyLevel;
    public DifficultySettings[] settings;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ApplyDifficultySettings()
    {
        int lvl = (int)difficultyLevel;
        //Debug.Log("Difficulty Level = " + lvl);
        settings[lvl].Apply();
    }
    public void ApplyDifficultySettings(EnemyData enemy)
    {
        int lvl = (int)difficultyLevel;
        //Debug.Log("Difficulty Level = " + lvl);
        settings[lvl].Apply(enemy);
    }
    public void ApplyDifficultySettings(PlayerData player)
    {
        int lvl = (int)difficultyLevel;
        //Debug.Log("Difficulty Level = " + lvl);
        settings[lvl].ApplyForPlayer(player);
    }
    public DifficultySettings GetCurrentDifficultySetting()
    {
        return settings[(int)difficultyLevel];
    }
}
