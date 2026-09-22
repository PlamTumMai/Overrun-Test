using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;

    public PlayerMovement playerMovement;
    public PlayerWeapon playerWeapon;

    public void UpgradeDamage()
    {
        playerWeapon.damageBonus += 10;

        ContinueGame();

        Debug.Log("Upgrade: DAMAGE +10");
    }

    public void UpgradeAttackSpeed()
    {
        playerWeapon.attackCooldown -= 0.2f;

        if (playerWeapon.attackCooldown < 0.2f)
        {
            playerWeapon.attackCooldown = 0.2f;
        }

        ContinueGame();

        Debug.Log("Upgrade: ATTACK SPEED");
    }

    public void UpgradeMoveSpeed()
    {
        playerMovement.moveSpeed += 1f;

        ContinueGame();

        Debug.Log("Upgrade: MOVE SPEED");
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