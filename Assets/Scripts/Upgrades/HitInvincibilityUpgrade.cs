using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInvincibilityUpgrade : MonoBehaviour
{
    [SerializeField] float percetageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().hitProtectionTime += other.GetComponent<PlayerHealthManager>().hitProtectionTime * percetageChange;

            SavePayerStats.hitProtectionTime = other.GetComponent<PlayerHealthManager>().hitProtectionTime;
            SavePayerStats.upgradeCount[10]++;
            Destroy(gameObject);
        }
    }
}
