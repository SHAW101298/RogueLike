using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public PlayerData data;

    [Space(10)]
    public Gun pistol;
    public Gun gun1;
    public Gun gun2;
    public Gun currentlySelected;
    int currentlySelectedIndex;

    public List<Gun> possesedGuns;
    public Transform crossHairPos;
    public Transform cameraPos;
    public Transform gunNozzle;

    [Space(20)]
    public PlayerUI ui;

    public bool trigger;

    // Update is called once per frame
    void Update()
    {
        ShootGun();            
    }

    public void GunTrigger(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            trigger = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            trigger = false;
        }
    }

    void ShootGun()
    {
        if(trigger == true)
        {
            if (currentlySelected == null)
            {
                return;
            }

            if(currentlySelected.modifiedStats.triggerType == ENUM_TriggerType.semi)
            {
                trigger = false;
            }
                
            currentlySelected.Shoot();  
            ui.UpdateAmmo();
            //ui.UpdateCrossHair();
        }
    }

    public void GunReloaded()
    {
        ui.UpdateAmmo();
    }
    public bool AttemptGunChange(Gun gun)
    {

        if(gun1 == null)
        {
            currentlySelected = gun1;
        }
        if(gun2 == null)
        {
            currentlySelected = gun2;
        }

        if(currentlySelected.canBeSwapped == false)
        {
            return false;
        }
        Debug.Log("Gun Can Be Swapped");



        currentlySelected.baseStats = gun.baseStats;
        
        currentlySelected.gunUpgrades = gun.gunUpgrades;
        currentlySelected.CreateModifiedStats();
        currentlySelected.RefillAmmoToMax();
        ui.UpdateAmmo();

        return true;
    }
    public bool AttemptGunChange3(Gun gun)
    {
        int index = 0;

        if (gun1 == null)
        {
            index = 0;
            currentlySelected = gun1;
        }
        if (gun2 == null)
        {
            index = 1;
            currentlySelected = gun2;
        }
        if(currentlySelected != null)
        {
            if (currentlySelected.canBeSwapped == false)
            {
                return false;
            }
        }

        currentlySelected = gun;
        currentlySelected.baseStats = gun.baseStats;

        currentlySelected.gunUpgrades = gun.gunUpgrades;
        currentlySelected.CreateModifiedStats();
        currentlySelected.RefillAmmoToMax();
        ui.UpdateAmmo();
        if(index == 0)
        {
            gun1 = gun;
        }
        else if (index == 1)
        {
            gun2 = gun;
        }
        return true;
    }
    public bool AttemptGunChange4(Gun newGun)
    {
        if(possesedGuns.Count < 3)
        {
            possesedGuns.Add(newGun);
            return true;
        }

        if(currentlySelected.canBeSwapped == false)
        {
            return false;
        }

        Gun oldGun = possesedGuns[currentlySelectedIndex];
        possesedGuns[currentlySelectedIndex] = newGun;
        currentlySelected = newGun;
        GunManager.Instance.CreateGunOnGround(oldGun, newGun.transform.position);

        //data.hookup.getHands;






        return true;
    }

    public void SetDataOnCharacterChange()
    {
        Debug.Log("Setting data on char change");
        cameraPos = CameraHookUp.Instance.gameObject.transform;
        crossHairPos = CameraHookUp.Instance.forwardPos;
    }
}
