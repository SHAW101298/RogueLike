using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_BlessingInfo : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public Image icon;

    public void UpdateInfo(string newTitle, string newDescription, int newIconID)
    {
        title.text = newTitle;
        description.text = newDescription;
        Debug.LogWarning("Implement icon list, to assign icon here");
    }
}
