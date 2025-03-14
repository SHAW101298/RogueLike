using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_WindowSizeCalculator : MonoBehaviour
{
    [SerializeField] TMP_Text textField;
    public RectTransform rectTransform;
    public RectTransform imageTransform;


    public void CalculateAndSetHeight()
    {
        Debug.Log("FOO");
        Debug.Log("preferredHeight is =  " + textField.preferredHeight);
        Vector2 size = rectTransform.sizeDelta;
        size = new Vector2(size.x, textField.preferredHeight);
        if(imageTransform != null)
        {
            if(size.y < imageTransform.sizeDelta.y)
            {
                Debug.Log(imageTransform.sizeDelta.y);
                size = new Vector2(size.x, imageTransform.sizeDelta.y);
            }
        }
        rectTransform.sizeDelta = size;
    }
}
