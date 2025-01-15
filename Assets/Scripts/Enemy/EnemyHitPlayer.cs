using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPlayer : MonoBehaviour
{
    [SerializeField] float hitDamage;
    [SerializeField] float cantHitTime;
    bool canDamage = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().PlayerDecreaseHealth(hitDamage);
            canDamage = false;
            StartCoroutine(ResetCanDamage());
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<EnemyCarMovement>().obsticle = true;
        }
    }
    IEnumerator ResetCanDamage()
    {
        yield return new WaitForSeconds(cantHitTime);

        canDamage = true;
    }
}
