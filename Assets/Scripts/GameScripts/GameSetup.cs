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
    [SerializeField] RoomGenerator roomGenerator;
    [SerializeField] List<RoomGenerator> generators;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Setting Up a Game");
        CreateBasicGuns();
        //roomGenerator.FirstRoomGeneration();
        foreach (RoomGenerator generator in generators)
        {
            generator.FirstRoomGeneration();
        }
        CreateMapForOtherPlayers();
        //GameOptions.Instance.ApplyDifficultySettings();
    }


    void CreateBasicGuns()
    {
        int possibleGuns = GunManager.Instance.gunList.Count;
        int randomVal;
        Gun tempGun;
        GunUpgradeBase upgrade;
        foreach(Transform spot in firstGunSpots)
        {
            tempGun = GunManager.Instance.CreateRandomGunOnGround(spot.position);
            upgrade = GunUpgradeRoller.ins.GetRandomRoll();
            tempGun.gunUpgrades.Add(upgrade);
            //randomVal = Random.Range(0, possibleGuns);
            //tempGun = GunManager.Instance.CreateGunOnGround(randomVal, spot.position);
        }
        //GunManager.Instance.CreateGunOnGround(possibleGuns, firstGunSpots[0].position);
    }

    public void CreateMapForOtherPlayers()
    {
        // If Im host, i dont request for map
        if(NetworkManager.Singleton.IsHost == true)
        {
            return;
        }

        roomGenerator.RequestMapLayout_ServerRPC(NetworkManager.Singleton.LocalClientId);
    }
}
