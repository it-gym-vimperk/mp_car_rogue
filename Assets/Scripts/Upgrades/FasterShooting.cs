using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FasterShooting : MonoBehaviour
{
    [SerializeField] float percetageChange;
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

            other.GetComponent<PlayerShoot>().timeBetweenShots -= other.GetComponent<PlayerShoot>().timeBetweenShots * percetageChange;


            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<TextMeshProUGUI>().text = showText;

            SavePayerStats.timeBetweenShots = other.GetComponent<PlayerShoot>().timeBetweenShots;
            SavePayerStats.upgradeCount[6]++;
            Destroy(gameObject);
        }
    }
}
