using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] LayerMask obsticleMask;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().PlayerDecreaseHealth(damage);
        }

        if (Physics.CheckSphere(transform.position, 0.1f, obsticleMask))
        {
            Destroy(gameObject);
        }

    }
}
