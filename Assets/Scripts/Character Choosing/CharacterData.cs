using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public GameObject cameraTarget;
    public GameObject character;
    public Gun pistol;
    [Header("Body")]
    public GameObject bodyObject;
    public GameObject bodyGunPosition;
    public Animator bodyAnim;
    [Header("Hands")]
    public GameObject handsObject;
    public GameObject handsGunPosition;
    public Animator handsAnim;

    public void DisableBodyObject()
    {
        Debug.Log("Disabling Body Object");
        bodyObject.SetActive(false);
    }
    public void EnableBodyObject()
    {
        Debug.Log("Enabling Body Object");
        bodyObject.SetActive(true);
    }
    public void DisableHandsObject()
    {
        Debug.Log("Disabling Hands Object");
        handsObject.SetActive(false);
    }
    public void EnableHandsObject()
    {
        Debug.Log("Enabling Hands Object");
        handsObject.SetActive(true);
    }
    
}
