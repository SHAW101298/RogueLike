using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarOfWeaponsInteractable : InteractableBase
{
    public Gun gun;
    public AltarOfWeapons altar;
    public GunPickup_InformationDeliver gunInfo;
    public Transform positionToPlaceWeapon;
    public bool isActive;

    public override void Interact(PlayerData data)
    {
        if (isActive == false)
            return;


        bool success = data.AttemptPickingGun(gun);



        if (success == true)
        {
            foreach (Transform child in transform)
            {
                //Debug.Log("me is = " + child.gameObject.name);
            }
            //Debug.Log("Destroying = " + gameObject.name);
            altar.GunChoosen(gun);
            //Destroy(gameObject);
        }
        
    }
    public void DisablePickUP()
    {
        //gun.gameObject.SetActive(false);
        gunInfo.gameObject.SetActive(false);
    }

}
