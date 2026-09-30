using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Debug.Log("EXIT GAME");

        Application.Quit();
    }
}