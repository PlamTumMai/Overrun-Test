using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject crawlerPrefab;
    public Transform player;

    public float spawnInterval = 2f;
    public float spawnDistance = 8f;

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnCrawler();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnCrawler()
    {
        if (player == null || crawlerPrefab == null)
            return;

        Vector2 spawnPosition = GetSpawnPosition();

        Instantiate(
            crawlerPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;

        return (Vector2)player.position +
               direction * spawnDistance;
    }
}