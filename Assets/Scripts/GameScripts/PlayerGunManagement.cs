using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunManagement : MonoBehaviour
{
    [Header("REF")]
    public PlayerData data;
    public PlayerUI ui;
    public List<Gun> possesedGuns;
    public Gun selectedGun;
    [SerializeField] Vector2 mouseScroll;

    int index = 0;
    public bool trigger;

    private void Start()
    {
        data = GetComponent<PlayerData>();
        ui = GetComponent<PlayerUI>();
    }

    private void Update()
    {
        ShootGun();
    }

    void ShootGun()
    {
        if (trigger == true)
        {
            if (selectedGun == null)
            {
                Debug.LogError("Selected Gun Is Null");
                //Debug.Log("Gun is Null");
                return;
            }

            if (selectedGun.modifiedStats.triggerType == ENUM_TriggerType.semi)
            {
                //Debug.Log("Gun is Semi");
                trigger = false;
            }

            selectedGun.Shoot();
            ui.UpdateAmmo();
            //ui.UpdateCrossHair();
        }
    }

    public void GunTrigger(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            trigger = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            trigger = false;
        }
    }

    public void SelectNextGun()
    {
        index++;
        if(index == possesedGuns.Count)
        {
            index = 0;
        }
        selectedGun.gameObject.SetActive(false);
        selectedGun = possesedGuns[index];
        selectedGun.gameObject.SetActive(true);
        data.currentGunTemplate.Value = selectedGun.presetID;

        data.GunScroll_ServerRPC(index, data.networkData.OwnerClientId);
        //ui.UpdateAmmo();
    }
    public void SelectPreviousGun()
    {
        index--;
        if(index < 0)
        {
            index = possesedGuns.Count - 1;
        }
        selectedGun.gameObject.SetActive(false);
        selectedGun = possesedGuns[index];
        selectedGun.gameObject.SetActive(true);
        data.currentGunTemplate.Value = selectedGun.presetID;
        data.GunScroll_ServerRPC(index, data.networkData.OwnerClientId);
        //ui.UpdateAmmo();
    }
    public void SelectGun(InputAction.CallbackContext context)
    {
        mouseScroll = context.ReadValue<Vector2>();

        // CurrentGunTemplate should only be modified here. Or Only on Owner.
        // Read current gun of other players from that value, and change accordingly

        if(context.performed == true)
        {
            data.events.OnWeaponSwapEvent.Invoke();
            //Debug.Log("Scroll performed");
            data.ui.HideReloadBar();

            if (mouseScroll.y > 0)
            {
                SelectNextGun();
            }
            else
            {
                SelectPreviousGun();
            }
            
            ui.UpdateAmmo();
        }

    }
    public void SelectGun(int i)
    {
        index = i;
        data.events.OnWeaponSwapEvent.Invoke();
        if(selectedGun != null)
        {
            selectedGun.gameObject.SetActive(false);
        }
        selectedGun = possesedGuns[index];
        selectedGun.gameObject.SetActive(true);
        data.currentGunTemplate.Value = selectedGun.presetID;
        ui.UpdateAmmo();
    }

    public bool TryPickingUpGun(Gun newGun)
    {
        if (possesedGuns.Count < 3)
        {
            //Debug.Log("NOT ENOUGH GUUUUUUNS");
            possesedGuns.Add(newGun);
            SelectGun(possesedGuns.Count-1);
            //newGun.playerData = data;
            newGun.transform.SetParent(data.characterData.handsGunPosition.transform);
            newGun.CatchReference(data);
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localEulerAngles = Vector3.zero;

            //Debug.Log("Picked Gun Preset is = " + newGun.presetID);
            data.NoticeAboutGunChange_ServerRPC(newGun.presetID, possesedGuns.Count - 1, data.OwnerClientId);

            return true;
        }

        if (selectedGun == possesedGuns[0])
        {
            //Debug.Log("false");
            return false;
        }



        //Debug.Log("Gotta change this one");
        Gun oldGun = possesedGuns[index];
        possesedGuns[index] = newGun;
        selectedGun = newGun;
        //newGun.playerData = data;
        //newGun.gunManagement = data.gunManagement;
        newGun.transform.SetParent(data.characterData.handsGunPosition.transform);
        newGun.CatchReference(data);

        GunManager.Instance.CreateGunOnGround(oldGun, newGun.transform.position);

        // Wait with setting these, before we create old gun in that spot
        newGun.transform.localEulerAngles = Vector3.zero;
        newGun.transform.localPosition = Vector3.zero;

        data.NoticeAboutGunChange_ServerRPC(newGun.presetID, index, data.networkData.OwnerClientId);

        return true;
    }
    public void GunReloaded()
    {
        ui.UpdateAmmo();
    }
    public void ReloadCurrentGun(InputAction.CallbackContext context)
    {
        if(context.performed == true)
        {
            selectedGun.ForceReload();
        }
    }
}
