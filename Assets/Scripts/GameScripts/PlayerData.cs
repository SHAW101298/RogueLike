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
    public PlayerGunManagement gunManagement;
    public PlayerUI ui;
    public PlayerInteractBeam interactionBeam;
    public PlayerInitialization initialization;
    public CharacterData characterData;
    public CameraHookUp cameraHookUp;
    public PlayerAmmunition ammo;

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

    public bool AttemptPickingGun(Gun gun)
    {
        Debug.Log("Attempting to pick gun");
        //bool success = shooting.AttemptGunChange3(gun);
        bool success = gunManagement.TryPickingUpGun(gun);

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
        SeperateGuns();
        Destroy(characterData.character.gameObject);

        characterData = temp.GetComponent<CharacterData>();
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.down;
        temp.transform.localEulerAngles = Vector3.zero;

        


        if (IsOwner == true)
        {
            CameraHookUp.Instance.Attach(gameObject);
            characterData.DisableBodyObject();
            characterData.EnableHandsObject();
            ui.HideCharacterSelector();
            stats.baseStats.CopyValues(characterData.stats);
            stats.CreateFinalStats();
            movement.SetMovementSpeed(characterData.moveSpeed);

            GameObject gun = Instantiate(characterData.pistol.gameObject);
            gunManagement.possesedGuns[0] = gun.GetComponent<Gun>();
            gunManagement.selectedGun = gunManagement.possesedGuns[0];
            gun.transform.SetParent(characterData.handsGunPosition.transform);
            gun.transform.localPosition = Vector3.zero;
            gunManagement.SelectGun(0);
            gunManagement.selectedGun.CatchReferences();
            ReattachGunsToHands();

        }
        else
        {
            characterData.DisableHandsObject();
            characterData.EnableBodyObject();
            ReattachGunsToBody();
        }

    }

    private void ReattachGunsToHands()
    {
        if (gunManagement.possesedGuns.Count > 1)
        {
            for (int i = 1; i < gunManagement.possesedGuns.Count; i++)
            {
                gunManagement.possesedGuns[i].gameObject.transform.SetParent(characterData.handsGunPosition.transform);
            }
        }
    }private void ReattachGunsToBody()
    {
        if (gunManagement.possesedGuns.Count > 1)
        {
            for (int i = 1; i < gunManagement.possesedGuns.Count; i++)
            {
                gunManagement.possesedGuns[i].gameObject.transform.SetParent(characterData.bodyGunPosition.transform);
            }
        }
    }

    private void SeperateGuns()
    {
        if (gunManagement.possesedGuns.Count > 1)
        {
            for (int i = 1; i < gunManagement.possesedGuns.Count; i++)
            {
                gunManagement.possesedGuns[i].gameObject.transform.SetParent(gameObject.transform);
            }
        }
    }

    /*
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
       characterData.DisableBodyObject();
       characterData.EnableHandsObject();
       movement.SetMovementSpeed(newCharacter.moveSpeed);
   }
   else
   {

   }


}
*/
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
