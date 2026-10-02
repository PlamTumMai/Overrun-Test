using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    private float playTime = 0f;

    void Start()
    {
        playTime = 0f;
        UpdateTimerUI();
    }

    void Update()
    {
        playTime += Time.deltaTime;

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(playTime / 60f);
        int seconds = Mathf.FloorToInt(playTime % 60f);

        timerText.text =
            string.Format(
                "{0:00}:{1:00}",
                minutes,
                seconds
            );
    }

    public float GetPlayTime()
    {
        return playTime;
    }
}