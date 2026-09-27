using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject energyOrbPrefab;

    public float attackCooldown = 1.5f;
    public float attackRange = 20f;

    public int damageBonus = 0;

    // จำนวนกระสุนที่ยิงต่อครั้ง
    public int projectileCount = 1;

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
        EnemyCrawler[] enemies = FindObjectsOfType<EnemyCrawler>();

        if (enemies.Length == 0)
            return;

        EnemyCrawler nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (EnemyCrawler enemy in enemies)
        {
            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < nearestDistance &&
                distance <= attackRange)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy == null)
            return;

        // ยิงหลายลูก
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject orb = Instantiate(
                energyOrbPrefab,
                transform.position,
                Quaternion.identity
            );

            EnergyOrb energyOrb =
                orb.GetComponent<EnergyOrb>();

            if (energyOrb != null)
            {
                energyOrb.damage += damageBonus;

                // กระจายเป้าหมายเล็กน้อย
                EnemyCrawler target = enemies[
                    Random.Range(0, enemies.Length)
                ];

                energyOrb.SetTarget(target.transform);
            }
        }
    }
}