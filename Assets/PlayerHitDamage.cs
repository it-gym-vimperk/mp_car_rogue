using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDamage : MonoBehaviour
{
    public float hitDamage;
    [SerializeField] float cantHitTime;
    bool canDamage = true;

    void Start()
    {
        hitDamage = SavePayerStats.hitDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canDamage)
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().EnemyDecreaseHealth(hitDamage);
            canDamage = false;
            StartCoroutine(ResetCanDamage());
        }
    }

    IEnumerator ResetCanDamage()
    {
        yield return new WaitForSeconds(cantHitTime);

        canDamage = true;
    }
}
