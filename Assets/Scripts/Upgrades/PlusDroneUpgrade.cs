using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusDroneUpgrade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InstantiateDrones>().InstantiateDrone();

            SavePayerStats.numberOfDrones += 1;
            SavePayerStats.upgradeCount[11]++;
            Destroy(gameObject);
        }
    }
}
