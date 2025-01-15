using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletUpgrade : MonoBehaviour
{
    [SerializeField] int plusFireDuration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().fireDuration += plusFireDuration;

            SavePayerStats.fireDuration = other.GetComponent<PlayerShoot>().fireDuration;
            SavePayerStats.upgradeCount[7]++;
            Destroy(gameObject);
        }
    }
}
