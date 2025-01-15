using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEnemies : MonoBehaviour
{
    [SerializeField] Collider[] enemies;
    [SerializeField] Transform target;
    [SerializeField] float checkRange;
    [SerializeField] LayerMask enemyMask;

    float smallestDis;

    [SerializeField] GameObject playerBullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxOffSet;
    [SerializeField] float waitBetweenShots;
    bool canShoot = true;
    void Update()
    {
        CheckForTarget();

        if(target != null && canShoot)
        {
            ShootAtTarget();
            canShoot = false;
        }

    }

    private void CheckForTarget()
    {
        enemies = Physics.OverlapSphere(transform.position, checkRange, enemyMask);
        smallestDis = 50;
        
        foreach(Collider enemy in enemies)
        {
            if(Vector3.Distance(enemy.transform.position, transform.position) < smallestDis)
            {
                smallestDis = Vector3.Distance(enemy.transform.position, transform.position);
                target = enemy.transform;
            }
        }
    }

    void ShootAtTarget()
    {
        GameObject insBul = Instantiate(playerBullet, transform.position + transform.forward * 0.1f, transform.rotation);

        Vector3 offSet = new Vector3(Random.Range(-maxOffSet, maxOffSet),
                         0, Random.Range(-maxOffSet, maxOffSet));

        Vector3 shootVector = (target.transform.position + offSet) - transform.position;

        insBul.GetComponent<Rigidbody>().AddForce(shootVector.normalized * bulletSpeed);

        StartCoroutine(canShootReset());

    }

    IEnumerator canShootReset()
    {
        yield return new WaitForSeconds(waitBetweenShots);
        canShoot = true;
    }
}
