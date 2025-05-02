using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class EndBoss : MonoBehaviour
{
    // STAGE 1 pro Bosse
    public int enemyToSpawn = 3;
    public GameObject[] typeOfEnemy;
    public Transform[] enemySpawnPositions;

    // STAGE 2 pro Bosse
    public GameObject cart;
    public Transform[] carSpawnPositions;

    // STAGE 3 pro BOSSE
    public UnityEvent eventForBoss;

    public float cartCooldown = 2.5f;
    private float lastUsedCartTime;

    public float enemyCooldown = 2.5f;
    private float lastUsedEnemyTime;

    private bool isActive;

    private int stageCycleCount = 0;
    private const int maxStageCycles = 6;
    bool firstStage = true;
    public BossStage currentStage;
    public GameObject bossPrefab;
    public GameObject endScreen;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            switch (currentStage)
            {
                case BossStage.Cart:
                    HandleCartStage();
                    break;
                case BossStage.Enemy:
                    HandleEnemyStage();
                    break;
                case BossStage.Boss:
                    HandleBossStage();
                    break;
            }
        }

        
        if(bossPrefab == null) {
            endScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void HandleCartStage()
    {
        SpawnCart();
        StartCoroutine(SwitchStageWithDelay(BossStage.Enemy));
    }

    private void HandleEnemyStage()
    {
        SpawnEnemies();
        if (stageCycleCount < maxStageCycles - 1)
        {
            StartCoroutine(SwitchStageWithDelay(BossStage.Cart));
            stageCycleCount++;
        }
        else
        {
            StartCoroutine(SwitchStageWithDelay(BossStage.Boss));
        }
    }

    private void HandleBossStage()
    {
        if(firstStage) {
            firstStage = false;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            GameObject[] carts = GameObject.FindGameObjectsWithTag("Damage");
            foreach (GameObject cart in carts)
            {
                Destroy(cart);
            }
            
            eventForBoss.Invoke();
        }
        //potom mozna pridat
    }

    private void SpawnCart()
    {
        if (Time.time > lastUsedCartTime + cartCooldown)
        {
            int randomIndex = Random.Range(0, carSpawnPositions.Length);
            Instantiate(cart, carSpawnPositions[randomIndex].position, Quaternion.identity);
            lastUsedCartTime = Time.time;
        }
    }

    private void SpawnEnemies()
    {
        if (Time.time > lastUsedEnemyTime + enemyCooldown)
        {
            for (int i = 0; i < enemyToSpawn; i++)
            {
                int randomEnemyIndex = Random.Range(0, typeOfEnemy.Length);
                int randomPositionIndex = Random.Range(0, enemySpawnPositions.Length);
                Instantiate(typeOfEnemy[randomEnemyIndex], enemySpawnPositions[randomPositionIndex].position, Quaternion.identity);
            }
            lastUsedEnemyTime = Time.time;
        }
    }

    private IEnumerator SwitchStageWithDelay(BossStage nextStage)
    {
        yield return new WaitForSeconds(15);
        currentStage = nextStage;
    }

    public void StartBoss()
    {
        isActive = true;
        currentStage = BossStage.Cart;
        stageCycleCount = 0;
    }
}

public enum BossStage
{
    Cart,
    Enemy,
    Boss
}
