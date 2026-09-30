using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    // Score ตอนเล่น
    public TMP_Text scoreText;

    // ข้อความสรุปตอน Game Over
    public TMP_Text resultText;

    // Game Timer
    public GameTimer gameTimer;

    private int enemyKills = 0;
    private int score = 0;

    void Start()
    {
        UpdateScoreUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // =========================
    // ADD KILL
    // =========================

    public void AddKill()
    {
        enemyKills++;

        // 1 Kill = 100 Score
        score += 100;

        UpdateScoreUI();

        Debug.Log(
            "Enemy Kill: " + enemyKills +
            " | Score: " + score
        );
    }

    // =========================
    // SCORE UI
    // =========================

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // =========================
    // GAME OVER
    // =========================

    public void GameOver()
    {
        Time.timeScale = 0f;

        // เวลาเล่นทั้งหมด
        float playTime = 0f;

        if (gameTimer != null)
        {
            playTime = gameTimer.GetPlayTime();
        }

        // ปัดเป็นจำนวนวินาที
        int playTimeSeconds =
            Mathf.FloorToInt(playTime);

        // คำนวณคะแนน
        int killScore =
            enemyKills * 100;

        int timeScore =
            playTimeSeconds * 10;

        int finalScore =
            killScore + timeScore;

        // เปิด Game Over Panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // แสดงผล
        if (resultText != null)
        {
            resultText.text =
                "Enemy Kills: " + enemyKills +
                "\nKill Score: " + killScore +
                "\nPlay Time: " + playTimeSeconds + " sec" +
                "\nTime Score: " + timeScore +
                "\nFinal Score: " + finalScore;
        }

        Debug.Log("========== GAME OVER ==========");
        Debug.Log("Enemy Kills: " + enemyKills);
        Debug.Log("Kill Score: " + killScore);
        Debug.Log("Play Time: " + playTimeSeconds + " sec");
        Debug.Log("Time Score: " + timeScore);
        Debug.Log("Final Score: " + finalScore);
    }

    // =========================
    // RESTART
    // =========================

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}