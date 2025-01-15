using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSizeUpgrade : MonoBehaviour
{

    [SerializeField] float percetageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().bulletSizeMultiplyer += other.GetComponent<PlayerShoot>().bulletSizeMultiplyer * percetageChange;

            SavePayerStats.bulletSizeMultiplyer = other.GetComponent<PlayerShoot>().bulletSizeMultiplyer;
            SavePayerStats.upgradeCount[8]++;
            Destroy(gameObject);
        }
    }
}
