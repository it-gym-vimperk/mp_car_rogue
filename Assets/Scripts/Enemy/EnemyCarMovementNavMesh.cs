using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCarMovementNavMesh : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Transform Enemy;
    [SerializeField] int maxDistance = 15;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(Vector3.Distance(Enemy.transform.position, transform.position) <= maxDistance)
        {
            agent.SetDestination(Player.position);
            agent.stoppingDistance = 0;

        }
        else
        {
            agent.SetDestination(Enemy.transform.position);
            agent.stoppingDistance = maxDistance;
        }
    }
}
