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
        NetworkManager.Singleton.SceneManager.OnLoadComplete += IntializePlayerScripts;
    }

    private void IntializePlayerScripts(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        if(IsOwner == true)
        {
            playerData.movement.enabled = true;
            playerData.rotation.enabled = true;
            playerData.ui.ammoCurrent = UI_HookUpScript.Instance.ammoCurrent;
            playerData.ui.magazineCurrent = UI_HookUpScript.Instance.magazineCurrent;
            playerData.ui.staminaBar = UI_HookUpScript.Instance.staminaBar;
        }
        else
        {
            Destroy(playerData.movement);
            Destroy(playerData.rotation);
            Destroy(playerData.ui);
            Destroy(playerData.gameObject.GetComponent<PlayerInput>());
        }
    }


}
