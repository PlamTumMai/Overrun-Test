using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject energyOrbPrefab;

    public float attackCooldown = 1f;
    public float attackRange = 10f;

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                Shoot(target);
                attackTimer = attackCooldown;
            }
        }
    }

    Transform FindNearestEnemy()
    {
        EnemyCrawler[] enemies = FindObjectsOfType<EnemyCrawler>();

        Transform nearestEnemy = null;
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
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    void Shoot(Transform target)
    {
        GameObject orb = Instantiate(
            energyOrbPrefab,
            transform.position,
            Quaternion.identity
        );

        EnergyOrb energyOrb = orb.GetComponent<EnergyOrb>();

        if (energyOrb != null)
        {
            energyOrb.SetTarget(target);
        }
    }
}