using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_RaycastedGunData : MonoBehaviour
{
    public TMP_Text damageField;
    public TMP_Text critField;
    public TMP_Text speedField;
    public TMP_Text magazineField;

    public Transform bonusesParent;
    public float spacesBetweenBonuses;

    [Header("Debug")]
    public bool runFunction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(runFunction == true)
        {
            runFunction = false;
            xx();
        }
    }

    public void xx()
    {
        Debug.Log("Running function on child count = " + bonusesParent.childCount);
        TMP_Text bonusInfo;
        float prefHeight;
        foreach(RectTransform bonus in bonusesParent)
        {
            bonusInfo = bonus.GetComponentInChildren<TMP_Text>();
            prefHeight = bonusInfo.preferredHeight;
            bonus.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, prefHeight);
            //bonus.ForceUpdateRectTransforms();
            //bonus.sizeDelta = new Vector2(bonus.rect.size.x, prefHeight);
            
        }
        bonusesParent.gameObject.SetActive(true);
        //currentLobbyName.preferredHeight;
        
    }
}
