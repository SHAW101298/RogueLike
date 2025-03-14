using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_DataField : MonoBehaviour
{
    public TMP_Text label;
    public TMP_Text data;
    public Image icon;

    public void UpdateBoth(string text, float val)
    {
        
    }
    public void UpdateBoth(string text, string val)
    {
        if (text != "")
        {
            label.text = text;
        }
        data.text = val.ToString();
    }
    public void UpdateAll(string text, int val, Sprite newIcon)
    {
        label.text = text;
        data.text = val.ToString();
        icon.sprite = newIcon;
    }
    public void UpdateAll(string text, string val, Sprite newIcon)
    {
        label.text = text;
        data.text = val;
        icon.sprite = newIcon;
    }
    public void UpdateBoth(string text, int val)
    {
        if (text != "")
        {
            label.text = text;
        }
        data.text = val.ToString();
    }
    public void UpdateValue(float val)
    {
        data.text = val.ToString();
    }
    public void UpdateValue(int val)
    {
        data.text = val.ToString();
    }
    public void UpdateLabel(string text)
    {
        label.text = text;
    }
}
