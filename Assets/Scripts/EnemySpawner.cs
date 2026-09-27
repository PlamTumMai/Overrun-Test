using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject crawlerPrefab;
    public Transform player;

    public float spawnInterval = 0.5f;
    public float spawnDistance = 8f;

    private float spawnTimer;
    private float gameTime;

    private int currentWave = 1;
    private bool changingWave = false;

    void Start()
    {
        spawnTimer = 0f;
        gameTime = 0f;
    }

    void Update()
    {
        // ถ้ากำลังเปลี่ยน Wave ห้าม Spawn
        if (changingWave)
            return;

        gameTime += Time.deltaTime;

        // เปลี่ยน Wave ทุก 60 วิ
        if (currentWave == 1 && gameTime >= 60f)
        {
            StartCoroutine(ChangeToWave2());
            return;
        }

        if (currentWave == 2 && gameTime >= 120f)
        {
            StartCoroutine(ChangeToWave3());
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnCrawler();

            if (currentWave == 1)
            {
                spawnInterval = 0.5f;
            }
            else if (currentWave == 2)
            {
                spawnInterval = 0.35f;
            }
            else
            {
                spawnInterval = 0.25f;
            }

            spawnTimer = spawnInterval;
        }
    }

    IEnumerator ChangeToWave2()
    {
        changingWave = true;

        Debug.Log("WAVE 1 COMPLETE!");

        // ล้างทุกอย่าง
        ClearStage();

        // รอให้สนามโล่ง
        yield return new WaitForSecondsRealtime(2f);

        currentWave = 2;

        // เริ่มนับเวลาต่อ
        spawnTimer = 1f;

        changingWave = false;

        Debug.Log("WAVE 2 START!");
    }

    IEnumerator ChangeToWave3()
    {
        changingWave = true;

        Debug.Log("WAVE 2 COMPLETE!");

        ClearStage();

        yield return new WaitForSecondsRealtime(2f);

        currentWave = 3;

        spawnTimer = 1f;

        changingWave = false;

        Debug.Log("WAVE 3 START!");
    }

    void ClearStage()
    {
        // ลบ Crawler
        EnemyCrawler[] enemies =
            FindObjectsOfType<EnemyCrawler>();

        foreach (EnemyCrawler enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        // ลบ EXP
        EXPOrb[] expOrbs =
            FindObjectsOfType<EXPOrb>();

        foreach (EXPOrb exp in expOrbs)
        {
            Destroy(exp.gameObject);
        }

        // ลบ Energy Orb
        EnergyOrb[] energyOrbs =
            FindObjectsOfType<EnergyOrb>();

        foreach (EnergyOrb orb in energyOrbs)
        {
            Destroy(orb.gameObject);
        }

        Debug.Log("STAGE CLEARED!");
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

        EnemyCrawler crawler =
            crawlerObject.GetComponent<EnemyCrawler>();

        if (crawler != null)
        {
            if (currentWave == 1)
            {
                crawler.enemyLevel = 1;
                crawler.maxHP = 20;
                crawler.damage = 10;
                crawler.moveSpeed = 2f;
            }
            else if (currentWave == 2)
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
        }
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 direction =
            Random.insideUnitCircle.normalized;

        return (Vector2)player.position
            + direction * spawnDistance;
    }
}