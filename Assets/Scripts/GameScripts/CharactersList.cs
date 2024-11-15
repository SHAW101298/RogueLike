using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
    public List<GameObject> charactersFPS;
    public List<RuntimeAnimatorController> controllers;

    public GameObject GetCharacter(string val)
    {
        int tempChar = Convert.ToInt32(val);

        return characters[tempChar];
    }
    public GameObject GetCharacterFirstPerson(string val)
    {
        int tempChar = Convert.ToInt32(val);
        return charactersFPS[tempChar];
    }
    public RuntimeAnimatorController GetController(string val)
    {
        int tempChar = Convert.ToInt32(val);

        return controllers[tempChar];
    }


    public void ChooseCharacter(int index)
    {
        PlayerData player = PlayerList.Instance.GetMyPlayer();
        CharacterData characterData = characters[index].gameObject.GetComponent<CharacterData>();
        player.ChangeCharacter(characterData);
    }
}
