using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateTeleporter : MonoBehaviour
{
    [SerializeField] GameObject bossFight;
    [SerializeField] Transform bossSpawnPos;

    [SerializeField] string showText;
    [SerializeField] GameObject GameManager;
    [SerializeField] GameObject textPanel;

    GameObject[] objectives;
    bool canBeActivated = false;
    bool hasBeenActivated;
    void Start()
    {
        InvokeRepeating("CheckForObjectives", 10f, 1f);
        GameManager = GameObject.FindGameObjectWithTag("GameMan");
        textPanel = GameManager.GetComponent<SavePayerStats>().textPanel;
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
        if(other.CompareTag("Player") && canBeActivated && !hasBeenActivated)
        {
            Instantiate(bossFight, bossSpawnPos.position, Quaternion.identity);
            hasBeenActivated = true;
        }
        else if(other.CompareTag("Player"))
        {
            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<TextMeshProUGUI>().text = showText;
        }
    }
}
