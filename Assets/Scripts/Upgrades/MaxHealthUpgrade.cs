using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUpgrade : MonoBehaviour
{
    [SerializeField] float plusMaxHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().maxHealth += plusMaxHealth;
            other.GetComponent<PlayerHealthManager>().PlayerDecreaseHealth(-plusMaxHealth);

            SavePayerStats.maxHealth = other.GetComponent<PlayerHealthManager>().maxHealth;
            SavePayerStats.upgradeCount[3]++;
            Destroy(gameObject);
        }
    }
}