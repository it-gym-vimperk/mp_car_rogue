using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamageUpgrade : MonoBehaviour
{
    [SerializeField] float percetageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHitDamage>().hitDamage += other.GetComponent<PlayerHitDamage>().hitDamage * percetageChange;

            SavePayerStats.hitDamage = other.GetComponent<PlayerHitDamage>().hitDamage;
            SavePayerStats.upgradeCount[1]++;
            Destroy(gameObject);
        }
    }
}
