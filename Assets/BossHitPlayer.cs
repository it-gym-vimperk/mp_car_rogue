using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitPlayer : MonoBehaviour
{
    [SerializeField] float hitDamage;
    [SerializeField] float cantHitTime;
    bool canDamage = true;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().PlayerDecreaseHealth(hitDamage);
            canDamage = false;
            StartCoroutine(ResetCanDamage());
            GetComponent<BossOneCarMovement>().obsticle = true;
        }

        Debug.Log("hit " + collision.gameObject.name);
    }

    IEnumerator ResetCanDamage()
    {
        yield return new WaitForSeconds(cantHitTime);

        canDamage = true;
    }
}
