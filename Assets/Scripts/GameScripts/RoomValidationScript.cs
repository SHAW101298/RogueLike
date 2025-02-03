using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomValidationScript : MonoBehaviour
{
    public int id;
    public LayerMask collisionMask;

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (Tools.CheckIfInMask(collisionMask, gameObject.layer) == false)
        {
            //Debug.Log(gameObject.name + " | " + "Collided with = " + other.gameObject.name + "\n RETURNING");
            return;
        }
        */

        if(Tools.CheckIfInMask(collisionMask, other.gameObject.layer) == true)
        {
            RoomManager roomManager = other.gameObject.GetComponentInParent<RoomManager>();
            int collisionID = roomManager.roomValidationScript.id;
            //Debug.Log(gameObject.name + " | " + "Collided with = " + other.gameObject.name);
            //Debug.Log(id + " | " + " Collided with " + collisionID);
            roomManager.floorParent.Alert_RoomCollide(id);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (Tools.CheckIfInMask(collisionMask, other.gameObject.layer) == true)
        {
            RoomManager roomManager = other.gameObject.GetComponentInParent<RoomManager>();
            int collisionID = roomManager.roomValidationScript.id;
            //Debug.Log(gameObject.name + " | " + "Collided with = " + other.gameObject.name);
            //Debug.Log(id + " | " + " Collided with " + collisionID);
            roomManager.floorParent.Alert_RoomCollide(id);
        }
    }
}
