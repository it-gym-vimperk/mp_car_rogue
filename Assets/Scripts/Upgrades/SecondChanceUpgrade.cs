using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChanceUpgrade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().secondChance++;

            SavePayerStats.secondChance++;
            SavePayerStats.upgradeCount[12]++;
            Destroy(gameObject);
        }
    }
}
