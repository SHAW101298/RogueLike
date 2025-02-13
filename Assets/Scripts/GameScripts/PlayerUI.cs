using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject characterChoosingWindow;
    public GameObject workShopWindow;
    public GameObject statusWindow;

    [Header("UI Objects")]
    public Text magazineCurrent;
    public Text ammoCurrent;
    public Image staminaBar;
    public Image healthBar;
    public Image shieldBar;
    public Image reloadBar;
    public GameObject reloadWindow;
    [Header("REF")]
    public PlayerGunManagement gunManagement;
    public CrossHairAnimation crossAnimation;
    public PlayerData data;



    public void UpdateAmmo()
    {
        if (enabled == false)
            return;

        magazineCurrent.text = gunManagement.selectedGun.magazineCurrent.ToString();
        ammoCurrent.text = data.ammo.GetCurrentAmmo(gunManagement.selectedGun.gunType).ToString();
    }
    public void UpdateCrossHair()
    {
        crossAnimation.TriggerShoot();
    }
    public void UpdateStaminaBar(float percentage)
    {
        staminaBar.fillAmount = percentage;
    }
    public void UpdateHealthBar(float percentage)
    {
        healthBar.fillAmount = percentage;
    }
    public void UpdateShieldBar(float percentage)
    {
        shieldBar.fillAmount = percentage;
    }
    public void UpdateReloadBar(float percentage)
    {
        reloadBar.fillAmount = percentage;
    }
    public void ShowReloadBar()
    {
        reloadWindow.SetActive(true);
    }
    public void HideReloadBar()
    {
        reloadWindow.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        data = gameObject.GetComponent<PlayerData>();
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void ShowCharacterSelector()
    {
        characterChoosingWindow.SetActive(true);
        ActivateMouse();
    }
    public void HideCharacterSelector()
    {
        characterChoosingWindow.SetActive(false);
        DisableMouse();
    }
    public void ShowWorkShopWindow()
    {
        workShopWindow.SetActive(true);
        ActivateMouse();
    }
    public void HideWorkShopWindow()
    {
        workShopWindow.SetActive(false);
        DisableMouse();
    }
    public void ShowStatusWindow(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if(statusWindow.activeSelf == false)
            {
                statusWindow.SetActive(true);
                ActivateMouse();
                UI_StatusWindow.Instance.UpdateWeaponData();
            }
            else
            {
                HideStatusWindow();
            }
        }
        
    }
    public void HideStatusWindow()
    {
        statusWindow.SetActive(false);
        DisableMouse();
    }

    void ActivateMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        data.movement.BlockMovement();
        data.rotation.BlockRotation();
        
    }
    void DisableMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        data.movement.AllowMovement();
        data.rotation.AllowRotation();
    }
}
