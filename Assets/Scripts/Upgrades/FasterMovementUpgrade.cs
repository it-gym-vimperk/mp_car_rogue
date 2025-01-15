using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterMovementUpgrade : MonoBehaviour
{
    [SerializeField] float percetageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCarMovement>().maxSpeed += other.GetComponent<PlayerCarMovement>().maxSpeed * percetageChange;

            SavePayerStats.maxSpeed = other.GetComponent<PlayerCarMovement>().maxSpeed;
            SavePayerStats.upgradeCount[4]++;
            Destroy(gameObject);
        }
    }
}
