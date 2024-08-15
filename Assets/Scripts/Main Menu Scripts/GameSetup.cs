using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    #region
    public static GameSetup Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion


    [SerializeField] Transform[] firstGunSpots;


    // Start is called before the first frame update
    void Start()
    {
        CreateBasicGuns();
    }


    void CreateBasicGuns()
    {
        int possibleGuns = GunManager.Instance.gunList.Count;
        int randomVal;
        Gun tempGun;
        foreach(Transform spot in firstGunSpots)
        {
            randomVal = Random.Range(0, possibleGuns);
            tempGun = GunManager.Instance.CreateGunOnGround2(randomVal, spot.position);
        }
        //GunManager.Instance.CreateGunOnGround(possibleGuns, firstGunSpots[0].position);
    }
}
