using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPickUp : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LootReward loot;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if(Tools.CheckIfInMask(playerLayer, other.gameObject.layer))
        {
            Debug.Log("In Mask");
            if(other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Is Player");
                PlayerData player = other.gameObject.GetComponent<PlayerData>();
                loot.GiveReward(player);
                Destroy(gameObject);
            }
        }
    }
}
