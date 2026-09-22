using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEXP : MonoBehaviour
{
    public int currentEXP = 0;
    public int requiredEXP = 5;
    public int level = 1;

    public Slider expBar;
    public TMP_Text levelText;
    public GameObject upgradePanel;

    void Start()
    {
        Time.timeScale = 1f;

        UpdateUI();

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        if (currentEXP >= requiredEXP)
        {
            LevelUp();
        }

        UpdateUI();
    }

    void LevelUp()
    {
        level++;

        currentEXP = 0;
        requiredEXP += 5;

        Debug.Log("LEVEL UP! Level " + level);

        UpdateUI();

        // หยุดเกม
        Time.timeScale = 0f;

        // เปิด Upgrade Panel
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

    void UpdateUI()
    {
        if (expBar != null)
        {
            expBar.maxValue = requiredEXP;
            expBar.value = currentEXP;
        }

        if (levelText != null)
        {
            levelText.text = "Level " + level;
        }
    }
}