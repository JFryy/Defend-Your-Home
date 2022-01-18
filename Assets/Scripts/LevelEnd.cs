using UnityEngine;
using TMPro;
using System;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI BombCounterText;
    public TextMeshProUGUI TurretCounterText;
    public GameObject upgradeScreen;
    public float levelLengthSeconds = 35;
    public float elapsedTime = 0;

    public int maxBombCount = 2;

    public int currentBombCount = 2;

    public int turretCount = 0;
    // make a dictionary storing price information this is going to be PITA to handle otherwise.
    public int smallRepairCost = 150;
    public int largeRepairCost = 350;

    public int smallRepairAmnt = 50;

    public int largeRepairAmnt = 100;

    public int bombUpgradeCost = 1500;

    public int turretCost = 1500;

    public int purchaseCount = 0;
    public int baseHealthUpgradeCost = 1000;

    public GameObject spawner;

    public int levelNumber = 1;

    public GameObject turretPrefab;
    public GameObject bombPrefab;
    public TextMeshProUGUI levelCounter;
    public TextMeshProUGUI secondsCounter;
    public GameObject mainBase;

    public GameObject explosionPrefab;

    public GameManager GameManager;

    public TextMeshProUGUI healthUpgradeText;

    private float timeSinceExplosion = 0; 

    private Vector3 endExplosionStart = new Vector3(6, -3, 1);

    void Start()
    {
        upgradeScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        timeSinceExplosion += Time.deltaTime;
        if (elapsedTime >= levelLengthSeconds)
        {
            KillAllEnemies();
            endExplosionStart = new Vector3(6, -3, 1);
            ShowUpgradeScreen();
            elapsedTime = 0;
            levelLengthSeconds += 1.5f;
            spawner.GetComponent<EnemySpawner>().secondsBetweenSpawn -= .08f;
            if (spawner.GetComponent<EnemySpawner>().secondsBetweenSpawn < 0.13f) {
                spawner.GetComponent<EnemySpawner>().secondsBetweenSpawn = 0.13f;
            }
        }
        levelCounter.text = "day:" + levelNumber.ToString();
        BombCounterText.text = currentBombCount.ToString();
        TurretCounterText.text = turretCount.ToString();
        secondsCounter.text = "time:" + Math.Truncate(levelLengthSeconds - elapsedTime).ToString();

        if (elapsedTime >= (levelLengthSeconds - 4) && timeSinceExplosion > 0.5){
            endExplosionStart.x -= 2.2f;
            Instantiate(explosionPrefab, endExplosionStart, Quaternion.identity);
            timeSinceExplosion = 0;
        }

    }

    void Update()
    {
        healthUpgradeText.text = "cost:" + (baseHealthUpgradeCost * (purchaseCount + 1)).ToString();

    }

    void ShowUpgradeScreen()
    {
        Time.timeScale = 0;
        upgradeScreen.gameObject.SetActive(true);

    }

    // Make a fancier function here that spawns a bunch of bombs and kills all enemies a few moments before the screen pops up.
    void KillAllEnemies()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Archer"))
        {
            Destroy(obj);
        }
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("TankEnemy"))
        {
            Destroy(obj);
        }
    }

    public void PurchaseRepairSmall()
    {
        if (GameManager.Instance.score >= smallRepairCost)
        {
            GameManager.Instance.SubtractPoints(smallRepairCost);
            mainBase.GetComponent<Base>().HealthRestored(smallRepairAmnt);
            Debug.Log("Repair Purchased.");
        }
    }

    public void PurchaseRepairLarge()
    {
        if (GameManager.Instance.score >= largeRepairCost)
        {
            GameManager.Instance.SubtractPoints(largeRepairCost);
            mainBase.GetComponent<Base>().HealthRestored(largeRepairAmnt);
            Debug.Log("Repair Purchased.");

        }
    }

    public void PurchaseHealthUpgrade()
    {
        if (GameManager.Instance.score >= baseHealthUpgradeCost * (purchaseCount + 1))
        {
            GameManager.Instance.SubtractPoints(baseHealthUpgradeCost * (purchaseCount + 1));
            mainBase.GetComponent<Base>().UpgradeHealth(100);
            purchaseCount += 1;
            Debug.Log("Health Upgrade Purchased.");

        }
    }

    public void PurchaseBombUpgrade() 
    {
        if (GameManager.Instance.score >= bombUpgradeCost) 
        {
            GameManager.Instance.SubtractPoints(bombUpgradeCost);
            maxBombCount += 1;
        }
    }

    public void PurchaseTurret() {
        if (GameManager.Instance.score >= turretCost) {
            GameManager.Instance.SubtractPoints(turretCost);
            turretCount += 1;

        }
    }

    public void ContinueFromUpgrade()
    {
        levelNumber += 1;
        Time.timeScale = 1;
        upgradeScreen.gameObject.SetActive(false);
        currentBombCount = maxBombCount;
        Debug.Log("Continue button pressed.");
    }

    public void SpawbBomb()
    {
        if (currentBombCount > 0){
            Instantiate(bombPrefab, Input.mousePosition, Quaternion.identity);
            currentBombCount -=1;
        }
    }

    public void SpawnTurretButton()
    {
        if (turretCount > 0) {
            Instantiate(turretPrefab, Input.mousePosition, Quaternion.identity);
            turretCount -= 1;
        }
    }

}

