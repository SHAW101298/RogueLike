using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgradeAmmoMax : GunUpgradeBase
{
    public int flatAmount = 0;
    public float percentageAmount = 1;

    [Header("Min-Max Amount")]
    public int flatMin = 50;
    public int flatMax = 250;
    public float percentageMin = 20;
    public float percentageMax = 100;

    public GunUpgradeAmmoMax()
    {
        upgradeType = ENUM_GunRandomUpgradeType.ammoMax;
    }

    public override void Roll()
    {
        int randInt = Random.Range(0, 2);
        float randFloat;

        if (randInt == 0)
        {
            randFloat = Random.Range(flatMin, flatMax);
            flatAmount = (int)randFloat;
        }
        else
        {
            randFloat = Random.Range(percentageMin, percentageMax);
            //percentageAmount = randFloat;
            percentageAmount = Mathf.Round(randFloat);
            percentageAmount /= 100;
        }
    }
}
