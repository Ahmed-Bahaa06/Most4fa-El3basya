using UnityEngine;
using KhosaryCode.Events;

namespace KhosaryCode.Scenes
{
    /// <summary>
    /// Placed in the initialization scene. 
    /// Automatically requests to load the Main Menu scene when the game starts.
    /// </summary>
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private GameSceneSO _mainMenuScene;
        
        [Header("Event Broadcasting")]
        [SerializeField] private LoadEventChannelSO _loadEventChannel;

        private void Start()
        {
#if UNITY_EDITOR
            // If we are playing in the editor and started from a different scene (e.g. testing Gameplay),
            // AutoBootstrapper loads us additively. We do NOT want to interrupt the test by forcing a Main Menu load.
            // Since SceneLoader moves this object to DontDestroyOnLoad in Awake, gameObject.scene.name becomes "DontDestroyOnLoad".
            // So we just check if the active scene is the BootStrapper itself.
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "BootStrapper")
            {
                return;
            }
#endif

            if (_mainMenuScene != null && _loadEventChannel != null)
            {
                _loadEventChannel.Invoke(_mainMenuScene);
            }
            else
            {
                Debug.LogError("[Bootstrapper] Missing references! Cannot load Main Menu.");
            }
        }
    }
}
