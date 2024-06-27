using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ErrorMessage : MonoBehaviour
{
    [SerializeField] GameObject errorWindow;
    [SerializeField] TMP_Text errorTextField;

    public void ShowErrorMessage(string text)
    {
        errorTextField.text = text;
        errorWindow.SetActive(true);
    }
    public void BTN_CloseWindow()
    {
        errorWindow.SetActive(false);
        errorTextField.text = "";
        Destroy(gameObject);
    }
}
