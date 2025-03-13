using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconsManager : MonoBehaviour
{
    public Image[] afflictionsIcons;
    public Image[] allIcons;

    public Image GetAfflictionIcon(ENUM_DamageType type)
    {
        return afflictionsIcons[(int)type];
    }
    public Image GetIcon(int x)
    {
        return allIcons[x];
    }
}
