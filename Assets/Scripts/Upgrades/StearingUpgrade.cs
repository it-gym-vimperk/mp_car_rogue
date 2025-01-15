using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StearingUpgrade : MonoBehaviour
{
    [SerializeField] float percetageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCarMovement>().stearingSpeed += other.GetComponent<PlayerCarMovement>().stearingSpeed * percetageChange;

            SavePayerStats.stearingSpeed = other.GetComponent<PlayerCarMovement>().stearingSpeed;
            SavePayerStats.upgradeCount[5]++;
            Destroy(gameObject);
        }
    }
}
