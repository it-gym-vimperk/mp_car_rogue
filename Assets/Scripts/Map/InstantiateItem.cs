using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateItem : MonoBehaviour
{
    [SerializeField] GameObject[] insItems;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * 10 + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)) * 10, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            int randomer = Random.Range(0, insItems.Length);

            Instantiate(insItems[randomer], transform.position + new Vector3(0,0.5f,0), Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
