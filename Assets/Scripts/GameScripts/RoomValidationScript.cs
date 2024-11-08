using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomValidationScript : MonoBehaviour
{
    public int id;
    public LayerMask collisionMask;

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(collisionMask, gameObject.layer) == false)
        {
            Debug.Log(gameObject.name + " | " + "Collided with = " + other.gameObject.name + "\n RETURNING");
            return;
        }

        if(Contains(collisionMask, other.gameObject.layer) == true)
        {
            int collisionID = other.gameObject.GetComponentInParent<RoomManager>().roomValidationScript.id;
            Debug.Log(gameObject.name + " | " + "Collided with = " + other.gameObject.name);
            //Debug.Log(id + " | " + " Collided with " + collisionID);
            RoomGenerator.Instance.Alert_RoomCollide(id);
        }
        
    }

    public bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

}
