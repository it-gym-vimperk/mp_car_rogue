using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDrones : MonoBehaviour
{
    [SerializeField] GameObject drone;
    int insDroneAtBeg;

    void Start()
    {
        for(int i = 0; i < insDroneAtBeg; i++)
        {
            InstantiateDrone();
        }
    }

    public void InstantiateDrone()
    {
        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Vector3 insPos = transform.position + randomPos;
        Instantiate(drone, insPos, Quaternion.identity);
    }
}
