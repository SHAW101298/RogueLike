using UnityEngine;

public class GunPickupInteract : InteractableBase
{
    public Gun thisGun;
    public override void Interact(PlayerData data)
    {
        //thisGun = GetComponentInChildren<Gun>();
        bool success = data.AttemptGunChange(thisGun);

        

        if (success == true)
        {
            thisGun.gameObject.transform.SetParent(data.shooting.gunNozzle.transform);
            //gameObject.transform.localPosition = Vector3.zero;
            thisGun.gameObject.transform.localEulerAngles = Vector3.zero;

            Vector3 nozzleCorrect = Vector3.zero - thisGun.nozzle.transform.localPosition;
            thisGun.gameObject.transform.localPosition = nozzleCorrect;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        thisGun = GetComponentInChildren<Gun>();
    }
}
