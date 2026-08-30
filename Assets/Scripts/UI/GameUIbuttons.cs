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

    public void ResumeGame()
    {
        KhosaryCode.Core.GameManager gm = FindAnyObjectByType<KhosaryCode.Core.GameManager>();
        if (gm != null)
        {
            gm.TogglePause();
        }
        else
        {
            // Failsafe if GameManager is missing
            Time.timeScale = 1f;
            KhosaryCode.UI.HUDManager hud = FindAnyObjectByType<KhosaryCode.UI.HUDManager>();
            if (hud != null) hud.HideAllPanels();
        }
    }
}

