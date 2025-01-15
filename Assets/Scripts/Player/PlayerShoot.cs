using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform shootAimer;
    [SerializeField] GameObject bullet;
    [SerializeField] float maxOffSet;

    float shootTimer;

    //shooting upgrades
    public int bulletSpeed;
    public float timeBetweenShots;
    public bool shootBackwards;

    //bulletStats
    public float damage;
    public int maxGoThroughEnemy;
    public float bulletSizeMultiplyer;
    public int fireDuration;

    private void Start()
    {
        Debug.Log(SavePayerStats.timeBetweenShots);
        timeBetweenShots = SavePayerStats.timeBetweenShots;
        Debug.Log(timeBetweenShots);

        shootBackwards = SavePayerStats.canShootBackwarts;

        damage = SavePayerStats.damage;
        maxGoThroughEnemy = SavePayerStats.maxGoThroughEnemy;
        bulletSizeMultiplyer = SavePayerStats.bulletSizeMultiplyer;
        fireDuration = SavePayerStats.fireDuration;
    }
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && shootTimer > timeBetweenShots)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    void Shoot()
    {
        GameObject insBul = Instantiate(bullet, transform.position + transform.forward * 0.1f, transform.rotation);

        Vector3 offSet = new Vector3(Random.Range(-maxOffSet,maxOffSet ),
                         0, Random.Range(-maxOffSet, maxOffSet));

        Vector3 shootVector = (shootAimer.position + offSet) - transform.position;

        GiveBulletStats(insBul, shootVector);

        if (shootBackwards)
        {
            GameObject insBul2 = Instantiate(bullet, transform.position - transform.forward * 0.1f, transform.rotation);
            GiveBulletStats(insBul2, -shootVector);
        }
    }

    void GiveBulletStats(GameObject bullet, Vector3 shootVector)
    {
        bullet.transform.localScale = bullet.transform.localScale * bulletSizeMultiplyer;
        bullet.GetComponent<PlayerBulletScript>().damage = damage;
        bullet.GetComponent<PlayerBulletScript>().maxGoThroughEnemy = maxGoThroughEnemy;
        bullet.GetComponent<PlayerBulletScript>().fireDuration = fireDuration;
        bullet.GetComponent<Rigidbody>().AddForce(shootVector.normalized * bulletSpeed);
    }
}
