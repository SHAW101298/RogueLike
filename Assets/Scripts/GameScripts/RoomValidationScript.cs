using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomValidationScript : MonoBehaviour
{
    public int id;
    public LayerMask collisionMask;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with = " + other.gameObject.name);
        if(Contains(collisionMask, other.gameObject.layer) == true)
        {
            int collisionID = other.gameObject.GetComponentInParent<RoomManager>().roomValidationScript.id;
            //Debug.Log(id + " | " + " Collided with " + collisionID);
            RoomGenerator.Instance.Alert_RoomCollide(collisionID);
        }
        
    }

    public bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

}
