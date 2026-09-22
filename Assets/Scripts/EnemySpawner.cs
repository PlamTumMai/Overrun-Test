using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject crawlerPrefab;
    public Transform player;

    public float spawnInterval = 2f;
    public float spawnDistance = 8f;
    public float gameTime = 0f;
    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnCrawler();

            if (gameTime < 60f)
            {
                spawnInterval = 2f;
            }
            else if (gameTime < 120f)
            {
                spawnInterval = 1f;
            }
            else
            {
                spawnInterval = 0.5f;
            }

            spawnTimer = spawnInterval;
        }
    }

    void SpawnCrawler()
    {
        if (player == null || crawlerPrefab == null)
            return;

        Vector2 spawnPosition = GetSpawnPosition();

        GameObject crawlerObject = Instantiate(
            crawlerPrefab,
            spawnPosition,
            Quaternion.identity
        );

        EnemyCrawler crawler = crawlerObject.GetComponent<EnemyCrawler>();

        if (crawler != null)
        {
            if (gameTime < 60f)
            {
                crawler.enemyLevel = 1;
                crawler.maxHP = 20;
                crawler.damage = 10;
                crawler.moveSpeed = 2f;
            }
            else if (gameTime < 120f)
            {
                crawler.enemyLevel = 2;
                crawler.maxHP = 30;
                crawler.damage = 12;
                crawler.moveSpeed = 2.2f;
            }
            else
            {
                crawler.enemyLevel = 3;
                crawler.maxHP = 45;
                crawler.damage = 15;
                crawler.moveSpeed = 2.5f;
            }

            crawler.currentHP = crawler.maxHP;

            Debug.Log(
                "Enemy Level: " + crawler.enemyLevel +
                " | HP: " + crawler.maxHP +
                " | Damage: " + crawler.damage
            );
        }
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;

        return (Vector2)player.position +
               direction * spawnDistance;
    }
}