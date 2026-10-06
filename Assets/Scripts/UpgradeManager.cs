using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;

    public PlayerMovement playerMovement;
    public PlayerWeapon playerWeapon;

    // =========================
    // HP +20
    // =========================

    public void UpgradeHP()
    {
        playerMovement.maxHP += 20;
        playerMovement.currentHP += 20;

        if (playerMovement.currentHP >
            playerMovement.maxHP)
        {
            playerMovement.currentHP =
                playerMovement.maxHP;
        }

        playerMovement.UpdateHPBar();

        Debug.Log(
            "Upgrade: HP +20 | Max HP: "
            + playerMovement.maxHP +
            " | Current HP: "
            + playerMovement.currentHP
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