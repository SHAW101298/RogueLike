using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
        GameObject characterObject = Instantiate(CharactersList.Instance.GetCharacter(character));
        SeperateGuns();
        Destroy(characterData.character.gameObject);

        characterData = characterObject.GetComponent<CharacterData>();
        

        if(IsOwner == true)
        {
            Debug.Log("Owner of changing Character");

            // Parent to Camera
            characterObject.transform.SetParent(CameraHookUp.Instance.gameObject.transform);
            characterObject.transform.localPosition = -characterData.cameraTarget.transform.localPosition;
            characterObject.transform.localEulerAngles = Vector3.zero;

            // Set Data
            CameraHookUp.Instance.Attach(gameObject);
            characterData.DisableBodyObject();
            characterData.EnableHandsObject();
            ui.HideCharacterSelector();
            stats.baseStats.CopyValues(characterData.stats);
            stats.CreateFinalStats();
            movement.SetMovementSpeed(characterData.moveSpeed);

            // Giving Guns Back
            GameObject gun = Instantiate(characterData.pistol.gameObject);
            gunManagement.possesedGuns[0] = gun.GetComponent<Gun>();
            gunManagement.selectedGun = gunManagement.possesedGuns[0];
            gun.transform.SetParent(characterData.handsGunPosition.transform);
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localEulerAngles = Vector3.zero;
            gunManagement.SelectGun(0);
            ReattachGunsToHands();
            gunManagement.selectedGun.CatchReference(this);
        }
        else
        {
            Debug.Log("Not Owner of character");

            // Parent to Body
            characterObject.transform.SetParent(gameObject.transform);
            characterObject.transform.localPosition = Vector3.zero;
            characterObject.transform.localEulerAngles = Vector3.zero;

            // Set Data
            characterData.DisableHandsObject();
            characterData.EnableBodyObject();

            // Giving Guns Back
            GameObject gun = Instantiate(characterData.pistol.gameObject);
            gunManagement.possesedGuns[0] = gun.GetComponent<Gun>();
            gunManagement.selectedGun = gunManagement.possesedGuns[0];
            gun.transform.SetParent(characterData.bodyGunPosition.transform);
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localEulerAngles = Vector3.zero;
            gunManagement.SelectGun(0);
            ReattachGunsToHands();
            gunManagement.selectedGun.CatchReference(this);
            ReattachGunsToBody();
        }
    }

    // Called for all players onto Specific Player Object
    public void ChangeCharacterOLD(int character)
    {
        GameObject temp = Instantiate(CharactersList.Instance.GetCharacter(character));
        SeperateGuns();
        Destroy(characterData.character.gameObject);

        characterData = temp.GetComponent<CharacterData>();
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.down;
        temp.transform.localEulerAngles = Vector3.zero;

        

        // If Owner attach to CameraHookUp
        if (IsOwner == true)
        {
            Debug.Log("Owner of changing Character");
            CameraHookUp.Instance.Attach(gameObject);
            characterData.DisableBodyObject();
            characterData.EnableHandsObject();
            ui.HideCharacterSelector();
            stats.baseStats.CopyValues(characterData.stats);
            stats.CreateFinalStats();
            movement.SetMovementSpeed(characterData.moveSpeed);

            // Giving Guns Back
            GameObject gun = Instantiate(characterData.pistol.gameObject);
            gunManagement.possesedGuns[0] = gun.GetComponent<Gun>();
            gunManagement.selectedGun = gunManagement.possesedGuns[0];
            gun.transform.SetParent(characterData.handsGunPosition.transform);
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localEulerAngles = Vector3.zero;
            gunManagement.SelectGun(0);
            gunManagement.selectedGun.CatchReference(this);
            ReattachGunsToHands();

        }
        // If not Owner Attach to PlayerObject
        else
        {
            Debug.Log("Not Owner of character");
            characterData.DisableHandsObject();
            characterData.EnableBodyObject();
            ReattachGunsToBody();
        }

    }

    private void ReattachGunsToHands()
    {
        if (gunManagement.possesedGuns.Count > 1)
        {
            GameObject temp;
            Debug.Log("Attaching Guns Hands");
            for (int i = 1; i < gunManagement.possesedGuns.Count; i++)
            {
                temp = gunManagement.possesedGuns[i].gameObject;
                temp.transform.SetParent(characterData.handsGunPosition.transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localEulerAngles = Vector3.zero;
            }
        }
    }
    private void ReattachGunsToBody()
    {
        if (gunManagement.possesedGuns.Count > 1)
        {
            GameObject temp;
            /// 
            Debug.Log("Attaching Guns Body");
            for (int i = 1; i < gunManagement.possesedGuns.Count; i++)
            {
                temp = gunManagement.possesedGuns[i].gameObject;
                temp.transform.SetParent(characterData.bodyGunPosition.transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localEulerAngles = Vector3.zero;
            }
        }
    }

    private void SeperateGuns()
    {
        if (gunManagement.possesedGuns.Count > 1)
        {
            Debug.Log("Seperating Guns");
            for (int i = 1; i < gunManagement.possesedGuns.Count; i++)
            {
                gunManagement.possesedGuns[i].gameObject.transform.SetParent(gameObject.transform);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void NoticeAboutGunChange_ServerRPC(int gunPreset,int gunSlot, ulong requestingPlayer)
    {
        Debug.Log("Notice about Gun Change SERVER RPC");
        PlayerList.Instance.GetPlayer(requestingPlayer).NoticeAboutGunChange_ClientRPC(gunPreset, gunSlot, requestingPlayer);
    }

    [ClientRpc]
    public void NoticeAboutGunChange_ClientRPC(int gunPreset, int gunSlot, ulong requestingPlayer)
    {
        Debug.Log("Notice About Gun Change CLIENT RPC | PRESET IS = " + gunPreset);
        if(requestingPlayer == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("IM the requesting Player");
            // I already changed my gun
            return;
        }

        if(gunManagement.possesedGuns.Count < gunSlot+1)
        {
            Debug.Log("Thats picked weapon");
            // Player Picked Up a Weapon
            Gun newGun = GunManager.Instance.CreateGun(gunPreset);
            newGun.gameObject.transform.SetParent(characterData.bodyGunPosition.transform);
            newGun.gameObject.transform.localPosition = Vector3.zero;
            newGun.gameObject.transform.localEulerAngles = Vector3.zero;
            gunManagement.possesedGuns.Add(newGun);
            gunManagement.SelectGun(gunSlot);
        }
        else
        {
            Debug.Log("Thats swapped weapon");
            // Player Swaps a Weapon
            Destroy(gunManagement.possesedGuns[gunSlot].gameObject);
            Gun newGun = GunManager.Instance.CreateGun(gunPreset);
            newGun.gameObject.transform.SetParent(characterData.bodyGunPosition.transform);
            newGun.gameObject.transform.localPosition = Vector3.zero;
            newGun.gameObject.transform.localEulerAngles = Vector3.zero;
            gunManagement.possesedGuns[gunSlot] = newGun;
            gunManagement.SelectGun(gunSlot);
        }
    }

    [ClientRpc]
    public void ChangeCharacter_ClientRPC(int character, ulong requestingPlayer)
    {
        Debug.Log(" CLIENTRPC | Character = " + character + "  |  RequestingPlaeyr = " + requestingPlayer);
        PlayerData player = PlayerList.Instance.GetPlayer(requestingPlayer);
        Debug.Log("network ownerclientID = " + player.networkData.OwnerClientId);
        player.ChangeCharacter(character);
        //PlayerList.Instance.GetPlayer(requestingPlayer).ChangeCharacter(character);

    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeCharacter_ServerRPC(int character, ServerRpcParams serverRpcParams = default)
    {
        ulong requestingId = serverRpcParams.Receive.SenderClientId;
        Debug.Log(" SERVERRPC | Character = " + character + "  |  Requesting ID = " + requestingId);
        ChangeCharacter_ClientRPC(character, requestingId);
    }


    [ServerRpc(RequireOwnership = false)]
    public void ShootBullet_ServerRPC(Vector3 dir , int gunSlot ,ulong requestingPlayer)
    {
        Debug.Log("Sending Phantom Bullet SERVER RPC from " + requestingPlayer);
        ShootBullet_ClientRPC(dir, gunSlot, requestingPlayer);
    }
    [ClientRpc]
    public void ShootBullet_ClientRPC(Vector3 dir, int gunSlot, ulong requestingPlayer)
    {
        Debug.Log("Sending Phantom Bullet CLIENT RPC from " + requestingPlayer);
        if(requestingPlayer == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("IM owner, i just shot");
            Debug.Log("Send Dir = " + dir);
            // Im the one shooting
            return;
        }

        // Shoot Phantom Bullet
        gunManagement.possesedGuns[gunSlot].CreatePhantomProjectile(dir);
    }

    [ServerRpc(RequireOwnership = false)]
    public void GunScroll_ServerRPC(int slot, ulong requestingPlayer)
    {
        Debug.Log("Player " + requestingPlayer + " scrolls into slot = " + slot);
        GunScroll_ClientRPC(slot, requestingPlayer);
    }
    [ClientRpc]
    public void GunScroll_ClientRPC(int slot, ulong requestingPlayer)
    {
        if(requestingPlayer == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("IM the scrolling one");
            return;
        }
        gunManagement.SelectGun(slot);
    }
}
