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

        private void Start()
        {
            HideAllPanels();
        }

        public void HideAllPanels()
        {
            if (_gamePauseHUD) _gamePauseHUD.SetActive(false);
            if (_comaHUD) _comaHUD.SetActive(false);
            if (_gameOverHUD) _gameOverHUD.SetActive(false);
        }

        public void ShowPauseHUD()
        {
            HideAllPanels();
            if (_gamePauseHUD) _gamePauseHUD.SetActive(true);
        }

        public void ShowComaHUD()
        {
            HideAllPanels();
            if (_comaHUD) _comaHUD.SetActive(true);
        }

        public void ShowGameOverHUD()
        {
            HideAllPanels();
            if (_gameOverHUD) _gameOverHUD.SetActive(true);
        }
    }
}
