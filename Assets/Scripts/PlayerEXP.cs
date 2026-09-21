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

    void Start()
    {
        UpdateUI();
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        Debug.Log("EXP: " + currentEXP + " / " + requiredEXP);

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

        Debug.Log("LEVEL UP! Level: " + level);
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