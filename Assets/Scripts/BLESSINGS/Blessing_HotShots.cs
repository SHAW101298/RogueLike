using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_HotShots : Blessing_Base
{
    PlayerData myPlayer;
    public int bulletCount;
    public DamageData damageData;
    int counter;

    public override void Apply()
    {
        myPlayer = GetComponentInParent<PlayerData>();
        myPlayer.events.playerShotBullet.AddListener(ShotCounter);
    }

    public override string GetDescription()
    {
        string text = "Every " +bulletCount + " shot deals additional " + damageData.damage + " fire damage.";
        return text;
    }
    public void ShotCounter()
    {
        Debug.Log("Counter works");
        counter++;
        if(counter >= bulletCount)
        {
            Debug.Log("Adding damage");
            myPlayer.events.lastShotBullet.bulletInfo.damageData.Add(damageData);
            counter = 0;
        }
    }

    private void Update()
    {
        
    }
}
