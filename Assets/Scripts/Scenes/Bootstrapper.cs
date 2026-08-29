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
