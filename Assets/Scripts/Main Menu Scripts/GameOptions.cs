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
        settings[lvl].Apply();
    }
}
