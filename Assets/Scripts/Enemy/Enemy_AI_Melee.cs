using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_Melee : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] NavMeshAgent agent;

    [Header("Debug")]
    [SerializeField] Transform movePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayers();
    }

    void MoveTowardsPlayers()
    {
        agent.SetDestination(movePosition.position);
        // Find Closest Player in range of 100
    }

}
