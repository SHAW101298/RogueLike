using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractBeam : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform cameraPosition;
    [SerializeField] Transform centerOfView;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask interactableLayers;

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Vector3 direction = centerOfView.position - cameraPosition.position;
            RaycastHit hitInfo;
            //Debug.Log("Interact Used");
            
            if(Physics.Raycast(cameraPosition.position, direction, out hitInfo, rayDistance, interactableLayers))
            {
                //Debug.Log("RayCast Returned HitInfo = " + hitInfo.collider.gameObject.name);
                InteractWithObject(hitInfo);
            }
            

        }
    }
    void InteractWithObject(RaycastHit hitInfo)
    {
        InteractableBase interactable = hitInfo.collider.GetComponent<InteractableBase>();
        interactable.Interact(playerData);
    }
}
