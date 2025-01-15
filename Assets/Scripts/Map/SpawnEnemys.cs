using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public float money;
    [SerializeField] float moneyPerSecond;
    [SerializeField] float plusMoneyPerSecond;
    [SerializeField] float maxMoneyPerSecond;
    [SerializeField] GameObject[] enemyesToSpawn;
    [SerializeField] int[] enemyPrices;
    [SerializeField] int enemyCap;
    GameObject[] Enemys;
    [SerializeField] LayerMask obsticleMask;

    Transform Player;
    [SerializeField] float spawnDis;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("TrySpawningEnemy", 0f, 1f);
    }

    private void Update()
    {
        money += moneyPerSecond * Time.deltaTime;


        if(maxMoneyPerSecond > moneyPerSecond)
        {
            moneyPerSecond += Time.deltaTime * plusMoneyPerSecond;
        }
        else
        {
            moneyPerSecond = maxMoneyPerSecond;
        }

    }

    void TrySpawningEnemy()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");

        if(Enemys.Length < enemyCap)
        {
            int randomEnemy = Random.Range(0, enemyesToSpawn.Length);
            Vector3 randomOffSet = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

            if(Physics.Raycast(Player.transform.position + randomOffSet.normalized * spawnDis,-transform.up, 3))
            {
                if (money >= enemyPrices[randomEnemy] && !Physics.CheckSphere(Player.transform.position + randomOffSet.normalized * spawnDis, 5, obsticleMask))
                {
                    money -= enemyPrices[randomEnemy];
                    Instantiate(enemyesToSpawn[randomEnemy], Player.transform.position + randomOffSet.normalized * spawnDis, Quaternion.identity);
                }
            }
        }
    }
}
