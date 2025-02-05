using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TESTINGSCRIPT : MonoBehaviour
{
    public EnemyData enemy;
    public Gun gun;

    [Header("Debug")]
    public bool runFunction;
    public bool runAddBlessingToPlayer;
    public bool runCOO;
    public bool runFOO;
    public bool runROO;

    [Header("FOO")]
    public GameObject teleportObject;
    public Transform teleportPosition;
    public PlayerData player;

    [Header("ROO")]
    public GameObject rotSource;
    public RoomManager room;
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(runCOO == true)
        {
            runCOO = false;
            COO();
        }
        if(runFOO == true)
        {
            runFOO = false;
            FOO();
        }
        if(runROO == true)
        {
            runROO = false;
            ROO();
        }
        if(runAddBlessingToPlayer == true)
        {
            runAddBlessingToPlayer = false;
            AddBlessingToPlayer();
        }
    }
    void COO()
    {
        Debug.LogWarning("COO");
        GunUpgradeBase upgrade = GunUpgradeRoller.ins.GetRandomRoll();
        gun.gunUpgrades.Add(upgrade);
        upgrade = GunUpgradeRoller.ins.GetRandomRoll();
        gun.gunUpgrades.Add(upgrade);
        gun.CreateModifiedStats();
        //RESIZETEST.ins.ShowData(gun);
        UI_RaycastedGunData.Instance.ShowGunData(gun);
    }
    void FOO()
    {
        player.TeleportPlayer(teleportPosition.position);
        //teleportObject.gameObject.transform.position = teleportPosition.position;
    }
    void ROO()
    {
        room.ActivateRoom();
    }
    void AddBlessingToPlayer()
    {

        player.blessings.AddBlessing(Blessings_Manager.Instance.blessings[0]);
        //player.blessings.list.Add(Blessings_Manager.Instance.blessings[0]);
    }

}
