using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] float playerHealth;
    public float healthRegen;
    public float hitProtectionTime;
    public int secondChance;
    GameObject gameOverScreen;

    TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    GameObject hitPanel;
    bool canTakeDamage = true;

    private void Start()
    {
        maxHealth = SavePayerStats.maxHealth;
        healthRegen = SavePayerStats.healthRegen;
        hitProtectionTime = SavePayerStats.hitProtectionTime;
        secondChance = SavePayerStats.secondChance;

        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;

        gameOverScreen = GameObject.FindGameObjectWithTag("GameOver");
        gameOverScreen.SetActive(false);

        hitPanel = GameObject.FindGameObjectWithTag("HitPanel");
        hitPanel.SetActive(false);
        playerHealth = maxHealth;

    }
    public void PlayerDecreaseHealth(float damage)
    {
        if (canTakeDamage)
        {
            hitPanel.SetActive(true);
            playerHealth -= damage;
            canTakeDamage = false;
            StartCoroutine(HitProtection());
        }

        if(playerHealth <= 0)
        {
            if (secondChance <= 0)
            {
                SavePayerStats.gameStarted = false;
                gameOverScreen.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                playerHealth = maxHealth;
                secondChance--;
                SavePayerStats.secondChance--;
                StartCoroutine(SpawnProtection());

            }
        }
    }

    private void Update()
    {
        healthText.text = (playerHealth * 10).ToString("0");
        healthSlider.maxValue = maxHealth;
        healthSlider.value = playerHealth;

        if (playerHealth < maxHealth)
        {
            playerHealth += healthRegen * Time.deltaTime;
        }
        else
        {
            playerHealth = maxHealth;
        }
    }
    IEnumerator HitProtection()
    {
        yield return new WaitForSeconds(hitProtectionTime);

        canTakeDamage = true;
    }

    IEnumerator SpawnProtection()
    {
        yield return new WaitForSeconds(hitProtectionTime * 5);

        canTakeDamage = true;
    }
}
