using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIbuttons : MonoBehaviour
{
 
    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

