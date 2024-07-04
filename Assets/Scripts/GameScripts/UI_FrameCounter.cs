using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_FrameCounter : MonoBehaviour
{
    public TMP_Text fpsField;
    int current;
    // Update is called once per frame
    void Update()
    {
        current = (int)(1f / Time.unscaledDeltaTime);
        fpsField.text = current.ToString();
    }
}
