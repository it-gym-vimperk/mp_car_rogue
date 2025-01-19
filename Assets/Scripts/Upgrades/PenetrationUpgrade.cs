using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PenetrationUpgrade : MonoBehaviour
{
    [SerializeField] int plusPenetration;
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

            other.GetComponent<PlayerShoot>().maxGoThroughEnemy += plusPenetration;

            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<TextMeshProUGUI>().text = showText;

            SavePayerStats.maxGoThroughEnemy = other.GetComponent<PlayerShoot>().maxGoThroughEnemy;
            SavePayerStats.upgradeCount[9]++;
            Destroy(gameObject);

        }
    }
}
