using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInteractBeam : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform cameraPosition;
    [SerializeField] Transform centerOfView;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask interactableLayers;

    UI_RaycastedGunData ui_RaycastedGunData;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            ui_RaycastedGunData = UI_RaycastedGunData.Instance;
        }
    }

    private void Update()
    {
        Foo();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Vector3 direction = centerOfView.position - cameraPosition.position;
            RaycastHit hitInfo;
            
            if(Physics.Raycast(cameraPosition.position, direction, out hitInfo, rayDistance, interactableLayers))
            {
                InteractWithObject(hitInfo);
            }
        }
    }

    void InteractWithObject(RaycastHit hitInfo)
    {
        InteractableBase interactable = hitInfo.collider.GetComponent<InteractableBase>();
        interactable.Interact(playerData);
    }

    public void Foo()
    {
        //Debug.LogWarning("Foo");
        RaycastHit hit;
        Vector3 dir = centerOfView.position - cameraPosition.position;
        if(Physics.Raycast(cameraPosition.position, dir, out hit, rayDistance, interactableLayers) == true)
        {
            // We definitely Hit something
            if (hit.collider.gameObject.CompareTag("Weapon"))
            {
                ui_RaycastedGunData.ShowGunData2(hit.collider.gameObject.GetComponent<Gun>());
            }
        }
    }
}
