using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject characterChoosingWindow;
    public GameObject workShopWindow;

    [Header("UI Objects")]
    public Text magazineCurrent;
    public Text ammoCurrent;
    public Image staminaBar;
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
