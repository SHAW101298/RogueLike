using UnityEngine;

public class GunPickupInteract : InteractableBase
{
    public override void Interact(PlayerData2 data)
    {
        Debug.Log("WE INTERACTING BOIS");
        Gun thisGun = GetComponent<Gun>();
        Debug.Log("This Gun = " + thisGun.gameObject.name);
        Debug.Log("Player = " + data.gameObject.name);
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
