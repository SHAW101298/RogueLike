using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInitialization : NetworkBehaviour
{
    [SerializeField] PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void InitializePlayerScripts()
    {
        Debug.LogWarning("Dodac zczytywanie nazw graczy. ");
        Debug.Log("Player Initialization for " + playerData.name);
        Debug.Log("Current scene = " + SceneManager.GetActiveScene().name);
        Debug.Log("is owner = " + IsOwner);
        if(IsOwner == true)
        {
            playerData.movement.enabled = true;
            playerData.rotation.enabled = true;
            playerData.ui.ammoCurrent = UI_HookUpScript.Instance.ammoCurrent;
            playerData.ui.magazineCurrent = UI_HookUpScript.Instance.magazineCurrent;
            playerData.ui.staminaBar = UI_HookUpScript.Instance.staminaBar;
            Debug.Log("Enabled my scripts");
        }
        else
        {
            playerData.movement.enabled = false;
            playerData.rotation.enabled = false;
            playerData.ui.enabled = false;
            playerData.rotation.camera.SetActive(false);
            playerData.gameObject.GetComponent<PlayerInput>().enabled = false;
            // Destroying creates network bugs
            //Destroy(playerData.movement);
            //Destroy(playerData.rotation);
            //Destroy(playerData.ui);
            //Destroy(playerData.gameObject.GetComponent<PlayerInput>());
            //Destroy(playerData.rotation.camera);
            Debug.Log("Disabled other player scripts");
        }
        CreateGameCharacter();
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            InitializePlayerScripts();
            enabled = false;
        }
    }

    void CreateGameCharacter()
    {
        Debug.Log("Creating Game Character");
        string charID = LobbyManager.Instance.currentPlayer.Data["Character"].Value;
        GameObject tempRef = CharactersList.Instance.GetCharacter(charID);
        GameObject tempGO = Instantiate(tempRef);
        tempGO.transform.SetParent(playerData.gameObject.transform);
        tempGO.transform.localEulerAngles = Vector3.zero;
        Animator tempAnim = playerData.gameObject.GetComponent<Animator>();
        tempAnim.runtimeAnimatorController = CharactersList.Instance.GetController(charID);
        tempAnim.enabled = true;
    }

}
