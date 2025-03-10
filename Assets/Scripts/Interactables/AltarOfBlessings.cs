using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarOfBlessings : MonoBehaviour
{
    public Blessing_Base blessing;
    public AltarOfBlessingsInteractable interactable;
    public BlessingPickup_InformationDeliver info;

    public void Start()
    {
        Debug.Log("Generating Blessing");
        GenerateBlessing();
    }
    public void DisableAltar()
    {
        interactable.gameObject.SetActive(false);
    }
    public void GenerateBlessing()
    {
        blessing = Blessings_Manager.Instance.GetRandomBlessing();
        info.blessing = blessing;
    }
}
