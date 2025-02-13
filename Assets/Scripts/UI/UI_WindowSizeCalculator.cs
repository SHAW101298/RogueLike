using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_WindowSizeCalculator : MonoBehaviour
{
    [SerializeField] TMP_Text textField;
    public RectTransform rectTransform;


    public void CalculateAndSetHeight()
    {
        Debug.Log("FOO");
        Debug.Log("preferredHeight is =  " + textField.preferredHeight);
        Vector2 size = rectTransform.sizeDelta;
        size = new Vector2(size.x, textField.preferredHeight);
        rectTransform.sizeDelta = size;
    }
}
