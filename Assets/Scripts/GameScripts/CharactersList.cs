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
    public GameObject GetCharacter(int val)
    {
        return characters[val];
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
        Debug.Log("Requesting Character Change");
        PlayerData player = PlayerList.Instance.GetMyPlayer();
        player.ChangeCharacter_ServerRPC(index, default);
        //CharacterData characterData = characters[index].gameObject.GetComponent<CharacterData>();
        //player.ChangeCharacter(characterData);
    }
    public void ChangeCharacterForPlayer(int index, PlayerData player)
    {
        //Debug.Log("Change Character for Other Player");
        player.ChangeCharacter(index);
    }
}
