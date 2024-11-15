using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [HideInInspector] public NetworkObject networkData;
    public PlayerStats stats;
    public PlayerMovement movement;
    public PlayerRotation rotation;
    public PlayerShooting shooting;
    public PlayerUI ui;
    public PlayerInteractBeam interactionBeam;
    public PlayerInitialization initialization;
    public CharacterData characterData;

    private void Awake()
    {
        networkData = gameObject.GetComponent<NetworkObject>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AttemptGunChange(Gun gun)
    {
        bool success = shooting.AttemptGunChange3(gun);
        return success;
    }
    public void TeleportPlayer(Vector3 pos)
    {
        movement.controller.enabled = false;
        gameObject.transform.position = pos;
        movement.controller.enabled = true;
        //transform.position = pos;
    }

    public void ChangeCharacter(int character)
    {
        GameObject temp = Instantiate(CharactersList.Instance.GetCharacter(character));
        Destroy(characterData.character.gameObject);

        characterData = temp.GetComponent<CharacterData>();
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.down;
        temp.transform.localEulerAngles = Vector3.zero;

        if (IsOwner == true)
        {
            CameraHookUp.Instance.Attach(gameObject);
            shooting.SetDataOnCharacterChange();
            characterData.DisableBodyObject();
            characterData.EnableHandsObject();
            ui.HideCharacterSelector();
        }
        else
        {
            shooting.SetDataOnCharacterChange();
            characterData.DisableHandsObject();
            characterData.EnableBodyObject();
        }
    }
    void ChangeCharacter(CharacterData newCharacter)
    {
        Debug.Log("Changing Character");
        ui.HideCharacterSelector();
        Destroy(characterData.character.gameObject);
        //characterData = newCharacter;

        GameObject temp = Instantiate(newCharacter.gameObject);
        characterData = temp.GetComponent<CharacterData>();
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.down;
        temp.transform.localEulerAngles = Vector3.zero;

        if(IsOwner == true)
        {
            CameraHookUp.Instance.Attach(gameObject);
            shooting.SetDataOnCharacterChange();
            characterData.DisableBodyObject();
            characterData.EnableHandsObject();
        }
        else
        {

        }

        
    }
    [ClientRpc]
    public void ChangeCharacter_ClientRPC(int character, ulong requestingPlayer)
    {
        Debug.Log(" CLIENTRPC | Character = " + character + "  |  RequestingPlaeyr = " + requestingPlayer);
        PlayerList.Instance.GetPlayer(requestingPlayer).ChangeCharacter(character);
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeCharacter_ServerRPC(int character, ServerRpcParams serverRpcParams = default)
    {
        ulong requestingId = serverRpcParams.Receive.SenderClientId;
        Debug.Log(" SERVERRPC | Character = " + character + "  |  Requesting ID = " + requestingId);
        ChangeCharacter_ClientRPC(character, requestingId);
    }
}
