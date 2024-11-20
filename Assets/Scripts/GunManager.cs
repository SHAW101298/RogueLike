using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    #region
    public static GunManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public List<GameObject> gunList;

    [Header("Prefabs")]
    [SerializeField] GameObject gunPickupPrefab;

    [Header("Debug")]
    public bool spawn;
    public Transform playerPos;

    public Gun CreateGun(int preset)
    {
        GameObject createdGunObject = Instantiate(gunList[preset]);
        Gun createdGun = createdGunObject.GetComponent<Gun>();
        return createdGun;
    }
    public Gun CreateGunOnGround(int preset, Vector3 pos)
    {
        GameObject gunPickupSpot = Instantiate(gunPickupPrefab);
        gunPickupSpot.transform.position = pos;
        gunPickupSpot.layer = 10;

        GameObject createdGunObject = Instantiate(gunList[preset]);
        Gun createdGun = createdGunObject.GetComponent<Gun>();
        createdGunObject.transform.SetParent(gunPickupSpot.transform);
        createdGun.transform.localPosition = Vector3.zero;

        GunPickupInteract interaction = gunPickupSpot.GetComponent<GunPickupInteract>();
        interaction.thisGun = createdGun;
        return createdGun;
    }
    public void CreateGunOnGround(Gun gun, Vector3 pos)
    {
        GameObject gunPickupSpot = Instantiate(gunPickupPrefab);
        gunPickupSpot.transform.position = pos;
        gunPickupSpot.layer = 10;

        gun.gameObject.transform.parent = null;
        gun.gameObject.transform.localPosition = Vector3.zero;
        GunPickupInteract interaction = gunPickupSpot.GetComponent<GunPickupInteract>();
        interaction.thisGun = gun;
        gun.gameObject.transform.SetParent(gunPickupSpot.transform);
        Debug.Log("Interaction gun = " + gun.gameObject.name);
    }

    public Gun CreateRandomGunOnGround(Vector3 pos)
    {
        int rand = Random.Range(0, gunList.Count);
        Gun gun = CreateGunOnGround(rand, pos);
        return gun;
    }
    private void Update()
    {
    }
}
