using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPointScript : MonoBehaviour
{
    public GameObject objectiveRacing;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectiveRacing.GetComponent<RandomObjective>().RaceObjective();
            Destroy(gameObject);
        }
    }
}
