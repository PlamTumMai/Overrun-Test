using UnityEngine;

public class EnemyCrawler : MonoBehaviour
{
    public int enemyLevel = 1;
    public float moveSpeed = 2f;

    public int maxHP = 20;
    public int currentHP;

    public int damage = 10;
    public GameObject expPrefab;

    private Transform player;

    void Start()
    {
        currentHP = maxHP;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        Vector3 direction = (player.position - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
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

        if (expPrefab != null)
        {
            Instantiate(
                expPrefab,
                transform.position,
                Quaternion.identity
            );
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