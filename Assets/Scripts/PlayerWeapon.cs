using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject energyOrbPrefab;

    public float attackCooldown = 1.5f;
    public float attackRange = 20f;

    public int damageBonus = 0;
    public int projectileCount = 1;

    public AudioSource audioSource;
    public AudioClip shootSound;

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            ShootMultiple();
            attackTimer = attackCooldown;
        }
    }

    void ShootMultiple()
    {
        EnemyCrawler[] enemies =
            FindObjectsOfType<EnemyCrawler>();

        EnemyCrawler nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // หา Enemy ที่อยู่ในจอและใกล้ที่สุด
        foreach (EnemyCrawler enemy in enemies)
        {
            if (enemy == null)
                continue;

            Vector3 viewport =
                Camera.main.WorldToViewportPoint(
                    enemy.transform.position
                );

            bool onScreen =
                viewport.z > 0 &&
                viewport.x >= 0 &&
                viewport.x <= 1 &&
                viewport.y >= 0 &&
                viewport.y <= 1;

            if (!onScreen)
                continue;

            float distance =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );

            if (distance <= attackRange &&
                distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy == null)
            return;

            if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // ทิศทางหลักไปหา Enemy
        Vector2 mainDirection =
            (nearestEnemy.transform.position -
             transform.position).normalized;

        // =========================
        // ยิงหลายลูก
        // =========================

        for (int i = 0; i < projectileCount; i++)
        {
            GameObject orb =
                Instantiate(
                    energyOrbPrefab,
                    transform.position,
                    Quaternion.identity
                );

            EnergyOrb energyOrb =
                orb.GetComponent<EnergyOrb>();

            if (energyOrb != null)
            {
                energyOrb.damage += damageBonus;

                // กระจายองศาการยิง
                float spread = 15f;

                float angle;

                if (projectileCount == 1)
                {
                    angle = 0f;
                }
                else
                {
                    angle =
                        Mathf.Lerp(
                            -spread,
                            spread,
                            (float)i /
                            (projectileCount - 1)
                        );
                }

                Vector2 direction =
                    RotateVector(
                        mainDirection,
                        angle
                    );

                energyOrb.SetDirection(direction);
            }
        }
    }

    Vector2 RotateVector(
        Vector2 vector,
        float angle
    )
    {
        float rad =
            angle * Mathf.Deg2Rad;

        float cos =
            Mathf.Cos(rad);

        float sin =
            Mathf.Sin(rad);

        return new Vector2(
            vector.x * cos -
            vector.y * sin,

            vector.x * sin +
            vector.y * cos
        ).normalized;
    }
}