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

    public void UpgradeProjectile()
    {
        playerWeapon.projectileCount += 1;

        ContinueGame();

        Debug.Log(
            "Upgrade: PROJECTILE +1 | Current: "
            + playerWeapon.projectileCount
        );
    }

    public void UpgradeMoveSpeed()
    {
        playerMovement.moveSpeed += 1f;

        ContinueGame();

        Debug.Log("Upgrade: MOVE SPEED +1");
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