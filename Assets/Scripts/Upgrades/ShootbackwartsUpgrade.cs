using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootbackwartsUpgrade : MonoBehaviour
{
    [SerializeField] float percentageChange;

    [SerializeField] string showText;
    GameObject textPanel;
    GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameMan");
        textPanel = GameManager.GetComponent<SavePayerStats>().textPanel;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<PlayerShoot>().shootBackwards = true;
            other.GetComponent<PlayerHealthManager>().healthRegen += other.GetComponent<PlayerHealthManager>().healthRegen * percentageChange;

            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<TextMeshProUGUI>().text = showText;

            SavePayerStats.canShootBackwarts = true;
            SavePayerStats.healthRegen = other.GetComponent<PlayerHealthManager>().healthRegen;
            SavePayerStats.upgradeCount[13]++;
            Destroy(gameObject);
        }
    }
}
