using UnityEngine;

public class EnemyCrawler : MonoBehaviour
{
    public float moveSpeed = 2f;

    public int maxHP = 20;
    public int currentHP;

    public int damage = 10;
    public int enemyLevel = 1;

    public SpriteRenderer spriteRenderer;

    // EXP
    public GameObject expPrefab;

    // HP BOX
    public GameObject hpBoxPrefab;
    public float hpBoxDropChance = 0.01f;

    private Transform player;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHP = maxHP;

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        Vector3 direction =
            (player.position - transform.position).normalized;

        transform.position +=
            direction * moveSpeed * Time.deltaTime;

        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;

        Debug.Log("Crawler HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

void Die()
{
    Debug.Log("Crawler Died!");

    // =========================
    // ADD SCORE
    // =========================

    GameOverManager gameOverManager =
        FindObjectOfType<GameOverManager>();

    if (gameOverManager != null)
    {
        gameOverManager.AddKill();
    }

    // =========================
    // EXP DROP
    // =========================

    int randomEXP = 0;
    bool isBigEXP = false;

    if (expPrefab != null)
    {
        float chance = Random.value;

        // 65% ไม่ดรอป
        if (chance < 0.65f)
        {
            Debug.Log("No EXP");
        }

        // 25% EXP ปกติ
        else if (chance < 0.90f)
        {
            randomEXP = Random.Range(2, 5);
            isBigEXP = false;
        }

        // 10% EXP ใหญ่
        else
        {
            randomEXP = Random.Range(6, 11);
            isBigEXP = true;
        }

        if (randomEXP > 0)
        {
            GameObject expObject = Instantiate(
                expPrefab,
                transform.position,
                Quaternion.identity
            );

            EXPOrb expOrb =
                expObject.GetComponent<EXPOrb>();

            if (expOrb != null)
            {
                expOrb.expValue = randomEXP;
                expOrb.isBigEXP = isBigEXP;
            }

            Debug.Log(
                "Dropped EXP: " + randomEXP +
                " | Big EXP: " + isBigEXP
            );
        }
    }

    // =========================
    // HP BOX DROP
    // =========================

    // จะสุ่ม HP Box เฉพาะกรณีที่ไม่ได้ EXP
    if (hpBoxPrefab != null && randomEXP <= 0)
    {
        float chance = Random.value;

        if (chance < hpBoxDropChance)
        {
            Instantiate(
                hpBoxPrefab,
                transform.position,
                Quaternion.identity
            );

            Debug.Log("Dropped HP BOX!");
        }
    }

    Destroy(gameObject);
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement =
                collision.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.TakeDamage(damage);
            }
        }
    }
}