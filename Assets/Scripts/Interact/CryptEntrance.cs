using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptEntrance : InteractableBase
{
    [SerializeField] BoxCollider collider;
    [SerializeField] LayerMask unitsLayer;
    public override void Interact(PlayerData data)
    {
        FOO();
    }

    void FOO()
    {
        RaycastHit[] hits;
        hits = Physics.BoxCastAll(collider.transform.position, collider.size / 2, Vector3.forward * 0.1f, transform.rotation, 0.1f, unitsLayer);

        if (hits.Length >= PlayerList.Instance.players.Count)
        {
            Debug.Log("Game Should Start");
            Debug.Log(hits.Length);
            foreach(RaycastHit hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }

}
