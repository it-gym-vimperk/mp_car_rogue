using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] float playerHealth;
    public float healthRegen;
    public float hitProtectionTime;
    public int secondChance;

    TextMeshProUGUI healthText;
    bool canTakeDamage = true;

    private void Start()
    {
        maxHealth = SavePayerStats.maxHealth;
        healthRegen = SavePayerStats.healthRegen;
        hitProtectionTime = SavePayerStats.hitProtectionTime;
        secondChance = SavePayerStats.secondChance;

        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
        playerHealth = maxHealth;
    }
    public void PlayerDecreaseHealth(float damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0 && canTakeDamage)
        {
            if (secondChance <= 0)
            {
                gameObject.SetActive(false);
                SavePayerStats.gameStarted = false;
                Debug.Log("LostMenu...");
            }
            else
            {
                playerHealth = maxHealth;
                secondChance--;
                SavePayerStats.secondChance--;
                StartCoroutine(SpawnProtection());

            }

            canTakeDamage = false;
            StartCoroutine(HitProtection());
        }
    }

    private void Update()
    {
        healthText.text = (playerHealth * 10).ToString("0");

        if(playerHealth < maxHealth)
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
