using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessings_Manager : MonoBehaviour
{
    #region
    public static Blessings_Manager Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public List<Blessing_Base> blessings;
}
