using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgradeReloadSpeed : GunUpgradeBase
{
    public float flatAmount = 0;
    public float percentageAmount = 1;

    [Header("Min-Max Amount")]
    public float flatMin = 0.1f;
    public float flatMax = 0.5f;
    public float percentageMin = 20;
    public float percentageMax = 100;

    public GunUpgradeReloadSpeed()
    {
        upgradeType = ENUM_GunRandomUpgradeType.reloadSpeed;
    }

    public override void Roll()
    {
        int randInt = Random.Range(0, 2);
        float randFloat;

        if (randInt == 0)
        {
            randFloat = Random.Range(flatMin, flatMax);
            flatAmount = randFloat;
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
