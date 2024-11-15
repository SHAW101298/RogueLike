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

    public void ChangeCharacter(CharacterData newCharacter)
    {
        Debug.Log("Changing Character");
        ui.HideCharacterSelector();
        Destroy(characterData.character.gameObject);
        characterData = newCharacter;

        GameObject temp = Instantiate(newCharacter.gameObject);
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.down;
        temp.transform.localEulerAngles = Vector3.zero;

        shooting.SetDataOnCharacterChange();
    }
}
