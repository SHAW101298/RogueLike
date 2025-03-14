using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsManager : MonoBehaviour
{
    #region
    public static IconsManager Instance;
    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public Sprite[] afflictionsIcons;
    public Sprite[] allIcons;

    public Sprite GetAfflictionIcon(ENUM_DamageType type)
    {
        return afflictionsIcons[(int)type];
    }
    public Sprite GetIcon(int x)
    {
        return allIcons[x];
    }
}
