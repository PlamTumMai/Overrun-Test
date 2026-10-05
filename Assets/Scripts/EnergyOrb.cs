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
            (Vector3)(direction * speed * Time.deltaTime);

        // ทำลายตัวเองเมื่อออกนอกจอ
        Vector3 viewport =
            Camera.main.WorldToViewportPoint(
                transform.position
            );

        if (viewport.x < -0.1f ||
            viewport.x > 1.1f ||
            viewport.y < -0.1f ||
            viewport.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(
        Collider2D collision
    )
    {
        EnemyCrawler enemy =
            collision.GetComponent<EnemyCrawler>();

        if (enemy != null)
        {
            // ทำ Damage แต่ไม่ทำลายกระสุน
            enemy.TakeDamage(damage);
        }
    }
}