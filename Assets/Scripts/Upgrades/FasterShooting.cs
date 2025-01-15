using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterShooting : MonoBehaviour
{
    [SerializeField] float percetageChange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().timeBetweenShots -= other.GetComponent<PlayerShoot>().timeBetweenShots * percetageChange;

            SavePayerStats.timeBetweenShots = other.GetComponent<PlayerShoot>().timeBetweenShots;
            SavePayerStats.upgradeCount[6]++;
            Destroy(gameObject);
        }
    }
}
