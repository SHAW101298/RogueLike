using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1HookUp : MonoBehaviour
{
    [Header("Body")]
    public Animator bodyanimator;
    public GameObject bodyObject;
    public GameObject bodyGunParent;
    [Header("Hands")]
    public Animator handsAnimator;
    public GameObject handsObject;
    public GameObject handsGunParent;


    public void Setup(bool isOwner)
    {
        if(isOwner == true)
        {
            bodyObject.SetActive(false);
            handsObject.SetActive(true);
            
            //gunHandle.transform.SetParent(handsGunParent.transform);
        }
        else
        {
            handsObject.SetActive(false);
            
        }
    }

}