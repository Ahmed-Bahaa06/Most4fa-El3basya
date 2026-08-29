using UnityEngine;

namespace KhosaryCode.UI
{
    /// <summary>
    /// Attach this to the Canvas to easily show/hide screens via Event Listeners.
    /// </summary>
    public class HUDManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject _gamePauseHUD;
        [SerializeField] private GameObject _comaHUD;
        [SerializeField] private GameObject _gameOverHUD;
        [SerializeField] private GameObject _gameWinHUD;

        private void Start()
        {
            HideAllPanels();
        }

        public void HideAllPanels()
        {
            if (_gamePauseHUD) _gamePauseHUD.SetActive(false);
            if (_comaHUD) _comaHUD.SetActive(false);
            if (_gameOverHUD) _gameOverHUD.SetActive(false);
            if (_gameWinHUD) _gameWinHUD.SetActive(false);
            
            // Failsafe: Always resume time when all panels are hidden
            Time.timeScale = 1f;
        }

        public void ShowPauseHUD()
        {
            HideAllPanels();
            if (_gamePauseHUD) _gamePauseHUD.SetActive(true);
            Time.timeScale = 0f; // Failsafe
        }

        public void ShowComaHUD()
        {
            HideAllPanels();
            if (_comaHUD) _comaHUD.SetActive(true);
            Time.timeScale = 0f; // Failsafe
        }

        public void ShowGameOverHUD()
        {
            HideAllPanels();
            if (_gameOverHUD) _gameOverHUD.SetActive(true);
            Time.timeScale = 0f; // Failsafe
        }

        public void ShowGameWinHUD()
        {
            HideAllPanels();
            if (_gameWinHUD) _gameWinHUD.SetActive(true);
            Time.timeScale = 0f; // Failsafe
        }
    }
}
