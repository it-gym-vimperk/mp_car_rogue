using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTeleporter : MonoBehaviour
{
    [SerializeField] GameObject bossFight;
    [SerializeField] Transform bossSpawnPos;

    GameObject[] objectives;
    bool canBeActivated = false;
    void Start()
    {
        InvokeRepeating("CheckForObjectives", 10f, 1f);
    }

    void CheckForObjectives()
    {
        objectives = GameObject.FindGameObjectsWithTag("Objectives");

        if(objectives.Length == 0)
        {
            canBeActivated = true;
            Debug.Log("Teleporter can be activated");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && canBeActivated)
        {
            Instantiate(bossFight, bossSpawnPos.position, Quaternion.identity);
        }
    }
}
