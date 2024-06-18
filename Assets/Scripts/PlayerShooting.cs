using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public Gun pistol;
    public Gun gun1;
    public Gun gun2;
    public Gun currentlySelected;

    public List<Gun> possesedGuns;
    public Transform crossHairPos;
    public Transform pointOfView;
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
            if(currentlySelected.modifiedStats.triggerType == ENUM_TriggerType.semi)
            {
                trigger = false;
            }
                
            currentlySelected.Shoot();  
            ui.UpdateAmmo();
            ui.UpdateCrossHair();
        }
    }

    public void GunReloaded()
    {
        ui.UpdateAmmo();
    }
    public bool AttemptGunChange(Gun gun)
    {
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
}
