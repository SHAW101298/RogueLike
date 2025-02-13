using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DataField : MonoBehaviour
{
    public TMP_Text label;
    public TMP_Text data;

    public void UpdateBoth(string text, float val)
    {
        if(text != "")
        {
            label.text = text;
        }
        data.text = val.ToString();
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
