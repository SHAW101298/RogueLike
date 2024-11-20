using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartZone : MonoBehaviour
{
    int currentPlayers;
    [SerializeField] BoxCollider collider;
    [SerializeField] float halfExtents;
    [SerializeField] LayerMask unitsLayer;
    private void LateUpdate()
    {
        currentPlayers = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            currentPlayers++;

            if(currentPlayers >= PlayerList.Instance.players.Count)
            {
                Debug.Log("Game Should Start now");
            }
        }
    }

    void FOO()
    {
        RaycastHit[] hits;
        hits = Physics.BoxCastAll(transform.position, collider.size / 2, Vector3.forward * 0.1f, transform.rotation, 0.1f, unitsLayer);

        if(hits.Length >= PlayerList.Instance.players.Count)
        {
            Debug.Log("Game Should Start");
        }
    }
}
