using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float maxShootAngle;
    [SerializeField] Transform Player;

    [SerializeField] GameObject enemyBullet;


    [SerializeField] Transform shootAimer;
    [SerializeField] float maxOffSet;

    [SerializeField] int bulletSpeed;

    [SerializeField] float timeBetweenShots;

    bool canShoot = true;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
    }

   
    void Update()
    {
        Vector3 dirToPlayer = Player.position - transform.position;

        if (Vector3.Angle(transform.forward, dirToPlayer) <= maxShootAngle && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject insProjectile = Instantiate(enemyBullet, transform.position, transform.rotation);

        Vector3 offSet = new Vector3(Random.Range(-maxOffSet, maxOffSet),
                 0, Random.Range(-maxOffSet, maxOffSet));

        Vector3 shootVector = (shootAimer.position + offSet) - transform.position;

        insProjectile.GetComponent<Rigidbody>().AddForce(shootVector.normalized * bulletSpeed);

        canShoot = false;

        StartCoroutine(canShootReset());
    }

    IEnumerator canShootReset()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }
}
