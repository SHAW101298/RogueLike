using UnityEngine;

public class GunPickupInteract : InteractableBase
{
    public Gun thisGun;
    public GunPickup_InformationDeliver info;
    public override void Interact(PlayerData data)
    {
        //thisGun = GetComponentInChildren<Gun>();
        bool success = data.AttemptPickingGun(thisGun);

        

        if (success == true)
        {
            foreach(Transform child in transform)
            {
                Debug.Log("me is = " + child.gameObject.name);
            }
            //Debug.Log("Destroying = " + gameObject.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        thisGun = GetComponentInChildren<Gun>();
    }
}
