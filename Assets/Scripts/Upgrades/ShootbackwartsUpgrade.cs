using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootbackwartsUpgrade : MonoBehaviour
{
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

            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<TextMeshProUGUI>().text = showText;

            SavePayerStats.canShootBackwarts = true;
            SavePayerStats.upgradeCount[13]++;
            Destroy(gameObject);
        }
    }
}
