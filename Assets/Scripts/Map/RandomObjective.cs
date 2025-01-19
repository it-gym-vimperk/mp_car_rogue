using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomObjective : MonoBehaviour
{
    public bool doingObjective = false;
    [SerializeField] int allObjectives;
    GameObject Player;
    GameObject[] allObjectivesGame;
    bool doneObjective;
    [SerializeField] GameObject Graphic;


    [SerializeField] string showText;
    GameObject textPanel;
    GameObject GameManager;


    #region race objective 
    [SerializeField] GameObject racingPoint;
    [SerializeField] int numberOfRacingPoints;
    [SerializeField] int mapSize;

    GameObject Arrow;
    int actNumOfRacingPoints;
    #endregion

    #region hold out objective
    [SerializeField] int timeToHouldout;
    [SerializeField] float loadingDistance;
    [SerializeField] GameObject loadCyl;
    [SerializeField] LayerMask groundMask;
    bool isHoldoutActive;
    Slider ProgressBar;
    float timer;
    #endregion

    private void Awake()
    {
        ProgressBar = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<Slider>();
    }
    private void Start()
    {
        ProgressBar.gameObject.SetActive(false);
        GameManager = GameObject.FindGameObjectWithTag("GameMan");
        textPanel = GameManager.GetComponent<SavePayerStats>().textPanel;
    }
    private void Update()
    {
        if (isHoldoutActive && Vector3.Distance(Player.transform.position, transform.position) <= loadingDistance)
        {
            HoldoutObjective();
        }

        if (doneObjective)
        {
            CompletedObjective();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        allObjectivesGame = GameObject.FindGameObjectsWithTag("Objectives");

        if (!doingObjective)
        {
            foreach (GameObject objective in allObjectivesGame)
            {
                if (objective.GetComponent<RandomObjective>().doingObjective)
                {
                    doingObjective = true;
                    break;
                }
                else
                {
                    doingObjective = false;
                }
            }
        }

        if (other.CompareTag("Player") && !doingObjective)
        {
            int randomer = Random.Range(0, allObjectives);
            doingObjective = true;

            Arrow = other.transform.GetChild(2).gameObject;
            Debug.Log(randomer);

            if (randomer == 0)
            {
                RaceObjective();
            }
            else
            {
                Player = other.gameObject;
                isHoldoutActive = true;
                ProgressBar.gameObject.SetActive(true);
                doingObjective = true;

                loadCyl.SetActive(true);
                loadCyl.transform.localScale = new Vector3(2 * loadingDistance, 0.01f, 2 * loadingDistance);
            }
        }
        else if (other.CompareTag("Player"))
        {
            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<TextMeshProUGUI>().text = showText;
        }
    }

    void CompletedObjective()
    {
        Graphic.SetActive(false);

        Arrow.SetActive(false);

        foreach (GameObject objective in allObjectivesGame)
        {
            objective.GetComponent<RandomObjective>().doingObjective = false;
        }

        Destroy(gameObject);
    }

    public void RaceObjective()
    {
        if(numberOfRacingPoints > actNumOfRacingPoints)
        {
            Vector3 randomPos = new Vector3(Random.Range(-mapSize / 2, mapSize / 2), 1, Random.Range(-mapSize / 2, mapSize / 2));

            if (!Physics.CheckSphere(randomPos, 4, groundMask))
            {
                GameObject insObject = Instantiate(racingPoint, randomPos, Quaternion.identity);
                insObject.GetComponent<RacingPointScript>().objectiveRacing = gameObject;

                Arrow.gameObject.SetActive(true);
                Arrow.GetComponent<LookAtPoint>().Target = insObject.transform;

                actNumOfRacingPoints += 1;
            }
            else
            {
                RaceObjective();
            }
        }
        else
        {
            doneObjective = true;
            Arrow.GetComponent<LookAtPoint>().Target = gameObject.transform;
        }
    }

    void HoldoutObjective()
    {
        timer += Time.deltaTime;
        ProgressBar.value = (timer / timeToHouldout) * 100;

        if(timer >= timeToHouldout)
        {
            doneObjective = true;
            ProgressBar.gameObject.SetActive(false);
        }
    }
}
