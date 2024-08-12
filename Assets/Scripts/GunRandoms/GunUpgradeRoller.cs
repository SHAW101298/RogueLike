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
        GU_DamagePercentage test = new GU_DamagePercentage();
        availableUpgrades.Add(test);
    }

    public void GetRandomRoll()
    {
        GunUpgradeBase randomUpgrade;
        int randomNumber = Random.Range(0, availableUpgrades.Count);
        randomUpgrade = availableUpgrades[randomNumber];
    }

    public void BuildUpgradesDatabase()
    {

    }

}
