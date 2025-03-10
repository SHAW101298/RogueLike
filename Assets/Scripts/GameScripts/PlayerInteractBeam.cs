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
    [SerializeField] LayerMask infoLayers;

    [SerializeField] UI_RaycastedGunData ui_RaycastedGunData;
    [SerializeField] UI_RayCastedBlessingInformation ui_RayCastedBlessingInformation;

    private void Start()
    {
        //Debug.Log("Player Interaction Beam START");
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            Debug.Log("Setting up reycast Gun Data");
            ui_RaycastedGunData = UI_RaycastedGunData.Instance;
            ui_RayCastedBlessingInformation = UI_RayCastedBlessingInformation.Instance;
        }
    }
    public void SetData()
    {
        cameraPosition = CameraHookUp.Instance.gameObject.transform;
        centerOfView = CameraHookUp.Instance.forwardPos;
        ui_RaycastedGunData = UI_RaycastedGunData.Instance;
        ui_RayCastedBlessingInformation = UI_RayCastedBlessingInformation.Instance;
    }

    private void Update()
    {
        RaycastForShowingGunData();
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

    public void RaycastForShowingGunData()
    {
        //Debug.LogWarning("Foo");
        RaycastHit hit;
        Vector3 dir = centerOfView.position - cameraPosition.position;
        if(Physics.Raycast(cameraPosition.position, dir, out hit, rayDistance, infoLayers) == true)
        {
            //Debug.Log("We hit = " + hit.collider.gameObject.name);
            // We definitely Hit something
            if(hit.collider.gameObject.CompareTag("GunPickUp"))
            {
                ui_RaycastedGunData.ShowGunData(hit.collider.gameObject.GetComponent<GunPickup_InformationDeliver>().gun);
                //ui_RaycastedGunData.ShowGunData(hit.collider.gameObject.GetComponent<GunPickupInteract>().thisGun);
            }
            
            switch(hit.collider.gameObject.tag)
            {
                case "GunPickUp":
                    ui_RaycastedGunData.ShowGunData(hit.collider.gameObject.GetComponent<GunPickup_InformationDeliver>().gun);
                    break;
                case "BlessingPickUp":
                    ui_RayCastedBlessingInformation.ShowData(hit.collider.gameObject.GetComponent<BlessingPickup_InformationDeliver>().blessing);
                    break;
                default:
                    Debug.LogError("Unkown Info Deliver switch case");
                    break;

            }
        }
    }

    void CheckInteractions()
    {

    }
}
