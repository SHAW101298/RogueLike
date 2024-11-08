using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomValidationScript : MonoBehaviour
{
    public int id;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with = " + other.gameObject.name);
        Debug.Log(id + " | " + " Collided with " + other.gameObject.GetComponentInParent<RoomManager>().roomValidationScript.id);
        RoomGenerator.Instance.Alert_RoomCollide(id);
    }

}
