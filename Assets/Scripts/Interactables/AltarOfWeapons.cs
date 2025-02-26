using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarOfWeapons : MonoBehaviour
{
    public List<AltarOfWeaponsInteractable> interactables;
    public int numberOfWeapons;
    public int numberOfUpgrades;

    public void GunChoosen(Gun gun)
    {
        foreach(AltarOfWeaponsInteractable interactable in interactables)
        {
            interactable.DisablePickUP();
            interactable.isActive = false;
        }
        gun.gameObject.SetActive(true);
    }

    private void Start()
    {
        GenerateGunsOnSpots();
    }
    public void GenerateGunsOnSpots()
    {
        Gun tempGun;
        for(int i = 0; i < numberOfWeapons; i++)
        {
            tempGun = GunManager.Instance.CreateRandomGun(numberOfUpgrades);
            tempGun.gameObject.transform.SetParent(interactables[i].positionToPlaceWeapon.gameObject.transform);
            tempGun.gameObject.transform.localPosition = Vector3.zero;
            interactables[i].gun = tempGun;
            interactables[i].gunInfo.gun = tempGun;
        }
    }
}
