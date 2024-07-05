using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<GameObject> gunList;

    [Header("Debug")]
    public bool spawn;
    public Transform playerPos;

    public Gun CreateGun(int preset)
    {
        GameObject createdGunObject = Instantiate(gunList[preset]);
        Gun createdGun = createdGunObject.GetComponent<Gun>();
        return createdGun;
    }
    public Gun CreateGunOnGround(int preset, Vector3 pos)
    {
        GameObject createdGunObject = Instantiate(gunList[preset]);
        createdGunObject.transform.position = pos;
        createdGunObject.layer = 10;

        Gun createdGun = createdGunObject.GetComponent<Gun>();
        GunPickupInteract createdGunInteract = createdGunObject.AddComponent<GunPickupInteract>();
        createdGunObject.AddComponent<GunFloatingScript>();
        createdGunInteract.interactableType = ENUM_InteractableType.gunPickup;

        return createdGun;
    }
    public void CreateGunOnGround(Gun gun, Vector3 pos)
    {
        gun.gameObject.transform.position = pos;
        gun.gameObject.transform.parent = null;
        GunPickupInteract createdGunInteract = gun.gameObject.AddComponent<GunPickupInteract>();
        createdGunInteract.interactableType = ENUM_InteractableType.gunPickup;
        gun.gameObject.layer = 10;
    }

    private void Update()
    {
        if(spawn == true)
        {
            spawn = false;
            Vector3 calculatedPos = playerPos.position;
            CreateGunOnGround(0, calculatedPos);
        }
    }
}
