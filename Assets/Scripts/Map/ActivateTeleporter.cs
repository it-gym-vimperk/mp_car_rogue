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

    TextMeshProUGUI objectiveDoneTxt;
    TextMeshProUGUI activateBossTxt;
    TextMeshProUGUI defeatBossTxt;
    void Start()
    {
        objectiveDoneTxt = GameObject.FindGameObjectWithTag("ObjectiveText").GetComponent<TextMeshProUGUI>();
        activateBossTxt = GameObject.FindGameObjectWithTag("ActivateBossText").GetComponent<TextMeshProUGUI>();
        defeatBossTxt = GameObject.FindGameObjectWithTag("DefeatBossText").GetComponent<TextMeshProUGUI>();

        GameManager = GameObject.FindGameObjectWithTag("GameMan");
        textPanel = GameManager.GetComponent<SavePayerStats>().textPanel;

        InvokeRepeating("CheckForObjectives", 0f, 1f);

        defeatBossTxt.text = "Defeat boss";
        defeatBossTxt.color = Color.grey;
    }



    void CheckForObjectives()
    {
        objectives = GameObject.FindGameObjectsWithTag("Objectives");
        
        if(objectives.Length == 0)
        {
            canBeActivated = true;
            Debug.Log("Teleporter can be activated");
            objectiveDoneTxt.text = "objective 3/3";
            objectiveDoneTxt.color = Color.grey;

            activateBossTxt.text = "activate boss";
            activateBossTxt.color = Color.white;

        }
        else
        {
            objectiveDoneTxt.text = "objective " + (3 - objectives.Length).ToString("0") + "/3";
            objectiveDoneTxt.color = Color.white;

            activateBossTxt.text = "activate boss";
            activateBossTxt.color = Color.grey;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && canBeActivated && !hasBeenActivated)
        {
            Instantiate(bossFight, bossSpawnPos.position, Quaternion.identity);
            hasBeenActivated = true;

            defeatBossTxt.color = Color.white;
            activateBossTxt.color = Color.grey;

            Destroy(gameObject);
        }
        else if(other.CompareTag("Player"))
        {
            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<TextMeshProUGUI>().text = showText;
        }
    }
}
