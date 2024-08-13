using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgradeRoller : MonoBehaviour
{
    public static GunUpgradeRoller ins;
    [SerializeField] public List<GunUpgradeBase> availableUpgrades;
    private void Awake()
    {
        ins = this;
    }

    public GunUpgradeBase GetRandomRoll()
    {
        GunUpgradeBase randomUpgrade;
        int randomNumber = Random.Range(0, availableUpgrades.Count);
        randomUpgrade = availableUpgrades[randomNumber];
        return randomUpgrade;
    }

    public void BuildUpgradesDatabase()
    {

    }

}
