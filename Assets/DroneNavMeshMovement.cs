using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneNavMeshMovement : MonoBehaviour
{
    [SerializeField] Transform Player;
    NavMeshAgent agent;

    [SerializeField] float stoppingDis;

    [SerializeField] LayerMask obsticleMask;
    bool newRandomPos = true;
    Vector3 randomPos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (newRandomPos)
        {
            randomPos = new Vector3(Random.Range(-stoppingDis / 2, stoppingDis / 2), 0, Random.Range(-stoppingDis / 2, stoppingDis / 2));
            newRandomPos = false;
        }

        Vector3 targetPos = Player.position + randomPos;

        agent.SetDestination(targetPos);

        if (Vector3.Distance(targetPos, transform.position) < 1f || Physics.CheckSphere(targetPos, 2, obsticleMask))
        {
            newRandomPos = true;
        }
    }
}
