using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenUpgrade : MonoBehaviour
{
    [SerializeField] float percatageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().healthRegen += other.GetComponent<PlayerHealthManager>().healthRegen * percatageChange;

            SavePayerStats.healthRegen = other.GetComponent<PlayerHealthManager>().healthRegen;
            SavePayerStats.upgradeCount[2]++;
            Destroy(gameObject);
        }
    }
}
