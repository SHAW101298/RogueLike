using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
        Debug.Log("Player Initialization");
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
            //Destroy(playerData.movement);
            //Destroy(playerData.rotation);
            //Destroy(playerData.ui);
            //Destroy(playerData.gameObject.GetComponent<PlayerInput>());
            //Destroy(playerData.rotation.camera);
            Debug.Log("Disabled other player scripts");
        }
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            InitializePlayerScripts();
            enabled = false;
        }
    }

}
