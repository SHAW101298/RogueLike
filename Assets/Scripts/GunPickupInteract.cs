using UnityEngine;

public class GunPickupInteract : InteractableBase
{
    [SerializeField] Vector3 rotateSpeed = new Vector3(0,0.1f,0);
    [SerializeField] float boopingSpeed = 0.05f;
    [SerializeField] float boopingHeight = 0.07f;
    [SerializeField] Vector3 boopingDir = Vector3.one;
    [SerializeField] Vector3 basePos;


    private void Update()
    {
        WobbleAnim();
    }
    void WobbleAnim()
    {
        transform.Translate(boopingSpeed * Time.deltaTime * boopingDir);
        transform.Rotate(rotateSpeed);
        if (transform.position.y >= basePos.y + boopingHeight || transform.position.y <= basePos.y - boopingHeight)
        {
            boopingDir *= -1;
        }

    }
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
