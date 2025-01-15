using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetUpper : MonoBehaviour
{
    [SerializeField] int setUpMoney;
    [SerializeField] int[] ChestPrize;
    [SerializeField] GameObject[] Chests;

    [SerializeField] float mapSizeX, mapSizeZ;
    [SerializeField] LayerMask groundMask;

    [SerializeField] GameObject objetive;
    [SerializeField] int howManyObjectives;
    void Awake()
    {
        SetupObjectives();

        while(setUpMoney > 0)
        {
            SetupChests();
        } 
    }

    void SetupChests()
    {
        int randomChest = Random.Range(0, Chests.Length);

        if (setUpMoney >= ChestPrize[randomChest])
        {
            Vector3 RandomPos = new Vector3(Random.Range(-mapSizeX / 2, mapSizeX / 2), 1, Random.Range(-mapSizeZ / 2, mapSizeZ / 2));

            if (!Physics.CheckSphere(RandomPos, 3, groundMask))
            {
                setUpMoney -= ChestPrize[randomChest];
                Instantiate(Chests[randomChest], RandomPos, Quaternion.identity);
            }

        }
    }

    void SetupObjectives()
    {
        for(int i = 0; i < howManyObjectives; i++)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-mapSizeX / 2, mapSizeX / 2), 1, Random.Range(-mapSizeZ / 2, mapSizeZ / 2));
          
            if (!Physics.CheckSphere(RandomPos, 3, groundMask))
            {
                Instantiate(objetive, RandomPos, Quaternion.identity); 
            }
            else
            {
                i--;
            }
        }
    }
        
}
