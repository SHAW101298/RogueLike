using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing_Athena_06 : Blessing_Base
{
    [SerializeField] float bonusMovementSpeed;
    [SerializeField] float timeOut;
    bool activeBlessing;
    float timer;
    public override void Apply()
    {
        GetParent();
        player.events.OnShieldDepletedEvent.AddListener(BlessingLogicShieldDepleted);
    }

    public override string GetDescription()
    {
        string text = "When shield is depleted, increase movement speed by " + bonusMovementSpeed + " for " + timeOut + " seconds.";
        return text;
    }

    public override void Remove()
    {
        player.events.OnShieldDepletedEvent.AddListener(BlessingLogicShieldDepleted);
        BlessingLogic_TimeOut();
    }

    public void BlessingLogicShieldDepleted()
    {
        Debug.Log("Activating " + title);
        if (activeBlessing == true)
        {
            return;
        }

        player.bonusStats.moveSpeed += bonusMovementSpeed;
    }
    public void BlessingLogic_TimeOut()
    {
        Debug.Log("Deactivating " + title);
        if (activeBlessing == false)
        {
            return;
        }
        player.bonusStats.moveSpeed -= bonusMovementSpeed;
    }
    private void Update()
    {
        if(activeBlessing == true)
        {
            timer += Time.deltaTime;
            if(timer >= timeOut)
            {
                timer = 0;
                activeBlessing = false;
                BlessingLogic_TimeOut();
            }
        }
    }
}
