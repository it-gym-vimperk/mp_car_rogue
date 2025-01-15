using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public float damage;
    public int fireDuration;
    [SerializeField] LayerMask obsticleMask;

    public int maxGoThroughEnemy;
    int goThroughEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthManager>().EnemyDecreaseHealth(damage);
            other.GetComponent<EnemyHealthManager>().FireDamageStarter(fireDuration);
            goThroughEnemy += 1;
        }

        if(goThroughEnemy >= maxGoThroughEnemy)
        {
            Debug.Log(goThroughEnemy);
            Destroy(gameObject);
        }

        if(Physics.CheckSphere(other.transform.position, 0.1f, obsticleMask) && !other.CompareTag("PlayerBullet"))
        {
            Debug.Log(other.name);
            Destroy(gameObject);
        }
    }
}
