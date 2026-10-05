using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject crawlerPrefab;
    public Transform player;

    // PlayerEXP ของตัว Player
    public PlayerEXP playerEXP;

    // =========================
    // SPAWN
    // =========================

    public float spawnInterval = 1f;
    public float spawnDistance = 8f;

    private float spawnTimer;
    private float gameTime;

    // =========================
    // WAVE
    // =========================

    public int currentWave = 1;

    private bool changingWave = false;

    void Start()
    {
        spawnTimer = 0f;
        gameTime = 0f;
        currentWave = 1;

        // ถ้าไม่ได้ลาก PlayerEXP มาใน Inspector
        if (playerEXP == null)
        {
            playerEXP =
                FindFirstObjectByType<PlayerEXP>();
        }
    }

    void Update()
    {
        if (changingWave)
            return;

        gameTime += Time.deltaTime;

        // =========================
        // WAVE ทุก 30 วินาที
        // =========================

        if (gameTime >= currentWave * 30f)
        {
            StartCoroutine(ChangeWave());
            return;
        }

        // =========================
        // SPAWN
        // =========================

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemies();

            spawnTimer = GetSpawnInterval();
        }
    }

    // =========================
    // CHANGE WAVE
    // =========================

    IEnumerator ChangeWave()
    {
        changingWave = true;

        Debug.Log(
            "WAVE " + currentWave +
            " COMPLETE!"
        );

        ClearStage();

        yield return new WaitForSecondsRealtime(2f);

        currentWave++;

        spawnTimer = 1f;

        changingWave = false;

        Debug.Log(
            "WAVE " + currentWave +
            " START!"
        );
    }

    // =========================
    // SPAWN ENEMIES
    // =========================

    void SpawnEnemies()
    {
        if (player == null ||
            crawlerPrefab == null)
        {
            return;
        }

        // Wave 1 = 1 ตัว
        // Wave 2 = 2 ตัว
        // Wave 3 = 3 ตัว
        // เป็นต้น

        int enemyCount =
            Mathf.Min(currentWave + 2,6);
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnCrawler();
        }   
    }

    // =========================
    // SPAWN CRAWLER
    // =========================

    void SpawnCrawler()
    {
        Vector2 spawnPosition =
            GetSpawnPosition();

        GameObject crawlerObject =
            Instantiate(
                crawlerPrefab,
                spawnPosition,
                Quaternion.identity
            );

        EnemyCrawler crawler =
            crawlerObject.GetComponent<EnemyCrawler>();

        if (crawler != null)
        {
            SetEnemyStats(crawler);
        }
    }

    // =========================
    // ENEMY STATS
    // =========================

    void SetEnemyStats(
        EnemyCrawler crawler
    )
    {
        // =========================
        // HP
        // ทุก 30 วิ × 1.25
        // =========================

        float hpMultiplier =
            Mathf.Pow(
                1.25f,
                currentWave - 1
            );

        crawler.maxHP =
            Mathf.RoundToInt(
                20 * hpMultiplier
            );

        crawler.currentHP =
            crawler.maxHP;

        // =========================
        // DAMAGE
        // =========================

        crawler.damage =
            10 +
            ((currentWave - 1) * 2);

        // =========================
        // MOVE SPEED
        // ใช้ PLAYER LEVEL
        // =========================

        int playerLevel = 1;

        if (playerEXP != null)
        {
            playerLevel =
                playerEXP.level;
        }

        // Level 1 = 3.0
        // Level 2 = 3.5
        // Level 3+ = 4.0

        if (playerLevel == 1)
        {
            crawler.moveSpeed = 3.0f;
        }
        else if (playerLevel == 2)
        {
            crawler.moveSpeed = 3.5f;
        }
        else
        {
            crawler.moveSpeed = 4.0f;
        }

        crawler.enemyLevel =
            currentWave;

        Debug.Log(
            "Enemy Spawned | " +
            "Wave: " + currentWave +
            " | Player Level: " + playerLevel +
            " | HP: " + crawler.maxHP +
            " | Damage: " + crawler.damage +
            " | Speed: " + crawler.moveSpeed
        );
    }

    // =========================
    // SPAWN INTERVAL
    // =========================

    float GetSpawnInterval()
    {
        // Wave สูงขึ้น
        // Spawn เร็วขึ้น

        float interval =
            1f -
            ((currentWave - 1) * 0.1f);

        // เร็วสุด 0.3 วิ
        return Mathf.Max(
            interval,
            0.3f
        );
    }

    // =========================
    // CLEAR STAGE
    // =========================

    void ClearStage()
    {
        // Enemy
        EnemyCrawler[] enemies =
            FindObjectsOfType<EnemyCrawler>();

        foreach (
            EnemyCrawler enemy
            in enemies
        )
        {
            Destroy(enemy.gameObject);
        }

        // EXP
        EXPOrb[] expOrbs =
            FindObjectsOfType<EXPOrb>();

        foreach (
            EXPOrb exp
            in expOrbs
        )
        {
            Destroy(exp.gameObject);
        }

        // Energy Orb
        EnergyOrb[] energyOrbs =
            FindObjectsOfType<EnergyOrb>();

        foreach (
            EnergyOrb orb
            in energyOrbs
        )
        {
            Destroy(orb.gameObject);
        }

        Debug.Log(
            "STAGE CLEARED!"
        );
    }

    // =========================
    // GET SPAWN POSITION
    // =========================

    Vector2 GetSpawnPosition()
    {
        Vector2 direction =
            Random.insideUnitCircle.normalized;

        return (Vector2)player.position +
               direction *
               spawnDistance;
    }
}