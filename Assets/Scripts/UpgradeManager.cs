using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;

    public PlayerMovement playerMovement;
    public PlayerWeapon playerWeapon;

    public void UpgradeMaxHP()
    {
        playerMovement.maxHP += 20;
        playerMovement.currentHP += 20;

        if (playerMovement.currentHP > playerMovement.maxHP)
        {
            playerMovement.currentHP = playerMovement.maxHP;
        }

        // อัปเดต HP Bar
        playerMovement.UpdateHPBar();

        Debug.Log("Upgrade: MAX HP +20");

        ContinueGame();
    }

    public void UpgradeProjectile()
    {
        playerWeapon.projectileCount += 1;

        Debug.Log(
            "Upgrade: PROJECTILE +1 | Current: "
            + playerWeapon.projectileCount
        );

        ContinueGame();
    }

    public void UpgradeMoveSpeed()
    {
        playerMovement.moveSpeed += 1f;

        Debug.Log("Upgrade: MOVE SPEED +1");

        ContinueGame();
    }

    void ContinueGame()
    {
        Time.timeScale = 1f;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }
}