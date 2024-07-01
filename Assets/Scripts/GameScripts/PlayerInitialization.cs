using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInitialization : NetworkBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
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
            playerMovement.enabled = true;
        }
    }


}
