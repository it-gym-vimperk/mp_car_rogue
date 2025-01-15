using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour
{
    [SerializeField] float percetageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().damage += other.GetComponent<PlayerShoot>().damage * percetageChange;

            SavePayerStats.damage = other.GetComponent<PlayerShoot>().damage;
            SavePayerStats.upgradeCount[0]++;
            Destroy(gameObject);
        }
    }
}
