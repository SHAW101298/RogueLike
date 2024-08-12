using UnityEngine;

public class GunPickupInteract : InteractableBase
{
    public override void Interact(PlayerData data)
    {
        Gun thisGun = GetComponent<Gun>();
        bool success = data.AttemptGunChange(thisGun);


        if (success == true)
        {
            gameObject.transform.SetParent(data.shooting.gunNozzle.transform);
            //gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localEulerAngles = Vector3.zero;

            Vector3 nozzleCorrect = Vector3.zero - thisGun.nozzle.transform.localPosition;
            gameObject.transform.localPosition = nozzleCorrect;
            Destroy(this);
        }
    }


}
