using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationUpgrade : MonoBehaviour
{
    [SerializeField] int plusPenetration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().maxGoThroughEnemy += plusPenetration;

            SavePayerStats.maxGoThroughEnemy = other.GetComponent<PlayerShoot>().maxGoThroughEnemy;
            SavePayerStats.upgradeCount[9]++;
            Destroy(gameObject);
        }
    }
}
