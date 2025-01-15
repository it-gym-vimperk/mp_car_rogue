using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootbackwartsUpgrade : MonoBehaviour
{

    [SerializeField] float percetageChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().shootBackwards = true;

            SavePayerStats.canShootBackwarts = true;
            SavePayerStats.upgradeCount[13]++;
            Destroy(gameObject);
        }
    }
}
