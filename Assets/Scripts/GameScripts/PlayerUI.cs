using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject characterChoosingWindow;

    [Header("UI Objects")]
    public Text magazineCurrent;
    public Text ammoCurrent;
    public Image staminaBar;
    [Header("REF")]
    public PlayerShooting shooting;
    public CrossHairAnimation crossAnimation;
    


    public void UpdateAmmo()
    {
        magazineCurrent.text = shooting.currentlySelected.magazineCurrent.ToString();
        ammoCurrent.text = shooting.currentlySelected.ammoCurrent.ToString();
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
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void ShowCharacterSelector()
    {
        characterChoosingWindow.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void HideCharacterSelector()
    {
        characterChoosingWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
