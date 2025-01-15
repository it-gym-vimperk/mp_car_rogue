using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] float enemyHealth;

    [SerializeField] int minKillMoney;
    [SerializeField] int maxKillMoney;

    [SerializeField] Slider healthSlider;
    [SerializeField] Image fillImage;

    float maxHealth;
    private void Start()
    {
        healthSlider.maxValue = enemyHealth;
        healthSlider.value = enemyHealth;
        fillImage.color = Color.green;

        maxHealth = enemyHealth;
    }

    public void EnemyDecreaseHealth(float damage)
    {
        enemyHealth -= damage;
        healthSlider.value = enemyHealth;

        #region Chatgptmade
        if (enemyHealth <= maxHealth / 3)
        {
            fillImage.color = Color.red; // Low health
        }
        else if (enemyHealth <= (2 * maxHealth) / 3)
        {
            fillImage.color = Color.yellow; // Mid health
        }
        else
        {
            fillImage.color = Color.green; // High health
        }
        #endregion

        if (enemyHealth <= 0)
        {
            SavePayerStats.moneySaver += Random.Range(minKillMoney, maxKillMoney);
            Destroy(transform.parent.gameObject);
        }
    }

    public void FireDamageStarter(float fireDuration)
    {
        StartCoroutine(FireDamage(fireDuration));
    }

    IEnumerator FireDamage(float fireDuration)
    {
        for (int i = 0; i < fireDuration; i++)
        {
            Debug.Log("Damage");
            EnemyDecreaseHealth(0.5f);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
