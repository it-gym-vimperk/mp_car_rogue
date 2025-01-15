using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField] int[] itemChances;
    [SerializeField] int priceToOpen;

    [SerializeField] GameObject[] insItems;

    [SerializeField] Material orgMat;
    [SerializeField] Material glowingMat;

    [SerializeField] GameObject ChestObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && SavePayerStats.moneySaver >= priceToOpen)
        {
            Debug.Log("Heloo");
            ChooseItems();

            SavePayerStats.moneySaver -= priceToOpen;

            Destroy(gameObject);
        }
    }

    void ChooseItems()
    {
        int randomer = Random.Range(0, 100);
        Vector3 insPos = transform.position;
        insPos.y = 1.5f;

        if(randomer < itemChances[0])
        {
            Instantiate(insItems[0], insPos, Quaternion.identity);
        }
        else if(randomer < itemChances[1])
        {
            Instantiate(insItems[1], insPos, Quaternion.identity);
        }
        else
        {
            Instantiate(insItems[2], insPos, Quaternion.identity);
        }
    }

    private void Update()
    {
        if(SavePayerStats.moneySaver >= priceToOpen)
        {
            ChestObj.GetComponent<MeshRenderer>().material = glowingMat;
        }
        else
        {
            ChestObj.GetComponent<MeshRenderer>().material = orgMat;
        }
    }
}
