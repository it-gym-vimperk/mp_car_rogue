using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePayerStats : MonoBehaviour
{
    #region Operating Variables
    public static int moneySaver;
    public static bool gameStarted = false;
    public static int[] upgradeCount;

    public static float musicVolume = 1;

    public GameObject textPanel;
    #endregion

    #region Stats
    public static float damage;
    public static float timeBetweenShots;
    public static float bulletSizeMultiplyer;
    public static bool canShootBackwarts;
    public static int maxGoThroughEnemy;
    public static int fireDuration;

    public static float hitDamage;
    public static float maxSpeed;
    public static float stearingSpeed;

    public static float maxHealth;
    public static float healthRegen;
    public static float hitProtectionTime;
    public static int secondChance;

    public static int numberOfDrones;
    #endregion


    #region Starting Variables
    [SerializeField] private float startingDamage;
    [SerializeField] private float startingTimeBetweenShots;
    [SerializeField] private float startingBulletSizeMultiplier;
    [SerializeField] private bool startingCanShootBackwards;
    [SerializeField] private int startingMaxGoThroughEnemy;
    [SerializeField] private int startingFireDuration;

    [SerializeField] private float startingHitDamage;
    [SerializeField] private float startingMaxSpeed;
    [SerializeField] private float startingSteeringSpeed;

    [SerializeField] private float startingMaxHealth;
    [SerializeField] private float startingHealthRegen;
    [SerializeField] private float startingHitProtectionTime;
    [SerializeField] private int startingSecondChance;

    [SerializeField] private int startingNumberOfDrones;

    [SerializeField] private int moneyStart;
    [SerializeField] int[] upgradeCountStart;
    #endregion

    private TextMeshProUGUI moneyText;
    [SerializeField] GameObject PlayerCanvas;
    [SerializeField] List<GameObject> upgradeImages;

    private void Awake()
    {
        textPanel = GameObject.FindGameObjectWithTag("TextPanel");
        moneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<TextMeshProUGUI>();

        if (!gameStarted)
        {
            ResetStats();
            gameStarted = true;
        }
    }
    private void Start()
    {
        PlayerCanvas = GameObject.FindGameObjectWithTag("UpgradePanel");

        for(int i = 0; i < PlayerCanvas.transform.childCount; i++)
        {
            upgradeImages.Add(PlayerCanvas.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        moneyText.text = moneySaver.ToString("0");

        ShowItems();
    }

    void ResetStats()
    {
        upgradeCount = upgradeCountStart;
        moneySaver = moneyStart;

        damage = startingDamage;
        timeBetweenShots = startingTimeBetweenShots;
        bulletSizeMultiplyer = startingBulletSizeMultiplier;
        canShootBackwarts = startingCanShootBackwards;
        maxGoThroughEnemy = startingMaxGoThroughEnemy;
        fireDuration = startingFireDuration;

        hitDamage = startingHitDamage;
        maxSpeed = startingMaxSpeed;
        stearingSpeed = startingSteeringSpeed;

        maxHealth = startingMaxHealth;
        healthRegen = startingHealthRegen;
        hitProtectionTime = startingHitProtectionTime;
        secondChance = startingSecondChance;

        numberOfDrones = startingNumberOfDrones;
    }

    void ShowItems()
    {
        for (int i = 0; i < PlayerCanvas.transform.childCount; i++)
        {
            if (upgradeImages[i].active)
            {
                upgradeImages[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgradeCount[i].ToString("0");
            }
        }
    }
}