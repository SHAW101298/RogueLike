using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharactersList : MonoBehaviour
{
    #region
    public static CharactersList Instance;
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
    public List<GameObject> characters;
    public List<AnimatorController> controllers;

    public GameObject GetCharacter(string val)
    {
        int tempChar = Convert.ToInt32(val);

        return characters[tempChar];
    }
    public AnimatorController GetController(string val)
    {
        int tempChar = Convert.ToInt32(val);

        return controllers[tempChar];
    }
}
