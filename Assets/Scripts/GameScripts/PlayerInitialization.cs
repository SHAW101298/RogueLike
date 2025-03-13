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
        SceneManager.sceneLoaded += OnSceneChanged;
        
        //Debug.Log("Start Is called");
        
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            //Debug.Log("Good Scene");
            InitializePlayerScripts();
            enabled = false;
        }
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Equals("SampleScene"))
        {
            //Debug.Log("Good Scene");
            InitializePlayerScripts();
            enabled = false;
        }
        else
        {
            Debug.Log("Wrong Scene = " + SceneManager.GetActiveScene().name);
        }
    }

    private void Awake()
    {
        //Debug.Log("Awake Is called");        
    }
    private void InitializePlayerScripts()
    {
        Debug.LogWarning("Dodac zczytywanie nazw graczy. ");
        Debug.Log("Player Initialization for " + playerData.name);
        //Debug.Log("Current scene = " + SceneManager.GetActiveScene().name);
        //Debug.Log("is owner = " + IsOwner);
        PlayerList.Instance.AddPlayer(playerData);
        //Debug.Log("Added player to list");

        if(IsOwner == true)
        {
            playerData.movement.enabled = true;
            playerData.rotation.enabled = true;
            CameraHookUp.Instance.Attach(playerData.gameObject);
            playerData.cameraHookUp = CameraHookUp.Instance;
            playerData.interactionBeam.SetData();
            playerData.ui.ammoCurrent = UI_HookUpScript.Instance.ammoCurrent;
            playerData.ui.magazineCurrent = UI_HookUpScript.Instance.magazineCurrent;
            playerData.ui.staminaBar = UI_HookUpScript.Instance.staminaBar;
            playerData.ui.healthBar = UI_HookUpScript.Instance.healthBar;
            playerData.ui.shieldBar = UI_HookUpScript.Instance.shieldBar;
            playerData.ui.reloadBar = UI_HookUpScript.Instance.reloadBar;
            playerData.ui.reloadWindow = UI_HookUpScript.Instance.reloadWindow;
            playerData.ui.characterChoosingWindow = UI_HookUpScript.Instance.characterSelector;
            playerData.ui.workShopWindow = UI_HookUpScript.Instance.workshop;
            playerData.ui.statusWindow = UI_HookUpScript.Instance.statusWindow;
            UI_HookUpScript.Instance.player = playerData;
            UI_StatusWindow.Instance.player = playerData;
            transform.position = Vector3.zero;
            
            //Debug.Log("Enabled my scripts");

            CharactersList.Instance.ChooseCharacter(0);
        }
        else
        {
            playerData.movement.enabled = false;
            playerData.rotation.enabled = false;
            playerData.interactionBeam.enabled = false;
            playerData.ui.enabled = false;
            //playerData.rotation.camera.SetActive(false);
            playerData.gameObject.GetComponent<PlayerInput>().enabled = false;
            playerData.gunManagement.enabled = false;
            // Destroying creates network bugs
            //Destroy(playerData.movement);
            //Destroy(playerData.rotation);
            //Destroy(playerData.ui);
            //Destroy(playerData.gameObject.GetComponent<PlayerInput>());
            //Destroy(playerData.rotation.camera);
            //Debug.Log("Disabled other player scripts");
            CharactersList.Instance.ChangeCharacterForPlayer(0,playerData);
            //RoomGenerator.Instance.RequestMapLayout_ServerRPC(NetworkManager.Singleton.LocalClientId);
        }
        //CreateGameCharacter();
    }






    [ClientRpc]
    void AbcdClientRpc(int framekey) { /* ... */ }

    [ClientRpc]
    void XyzwClientRpc(int framekey, ClientRpcParams clientRpcParams = default) { /* ... */ }





}
