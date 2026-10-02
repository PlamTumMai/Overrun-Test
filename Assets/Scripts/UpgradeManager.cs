using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;

    public PlayerMovement playerMovement;
    public PlayerWeapon playerWeapon;

    // =========================
    // DAMAGE +20
    // =========================

    public void UpgradeDamage()
    {
        playerWeapon.damageBonus += 20;

        Debug.Log(
            "Upgrade: DAMAGE +20 | Current Bonus: "
            + playerWeapon.damageBonus
        );

        ContinueGame();
    }

    // =========================
    // PROJECTILE +1
    // =========================

    public void UpgradeProjectile()
    {
        playerWeapon.projectileCount += 1;

        Debug.Log(
            "Upgrade: PROJECTILE +1 | Current: "
            + playerWeapon.projectileCount
        );

        ContinueGame();
    }

    // =========================
    // MOVE SPEED +1
    // =========================

    public void UpgradeMoveSpeed()
    {
        playerMovement.moveSpeed += 1f;

        Debug.Log(
            "Upgrade: MOVE SPEED +1 | Current: "
            + playerMovement.moveSpeed
        );

        ContinueGame();
    }

    // =========================
    // CONTINUE
    // =========================

    void ContinueGame()
    {
        Time.timeScale = 1f;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }
}