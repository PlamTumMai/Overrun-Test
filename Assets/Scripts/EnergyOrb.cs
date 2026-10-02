using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 20;

    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Update()
    {
        transform.position +=
            (Vector3)direction *
            speed *
            Time.deltaTime;
    }

    private void OnTriggerEnter2D(
        Collider2D collision
    )
    {
        EnemyCrawler enemy =
            collision.GetComponent<EnemyCrawler>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}