using UnityEngine;
using UnityEngine.SceneManagement;
using KhosaryCode.Events;
using System.Collections;

namespace KhosaryCode.Scenes
{
    /// <summary>
    /// Listens to LoadEventChannelSO and loads scenes asynchronously.
    /// Should be placed on a persistent Bootstrapper or Managers GameObject.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [Header("Event Listening")]
        [SerializeField] private LoadEventChannelSO _loadEventChannel;

        private static SceneLoader _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            // Ensure the SceneLoader persists across scene loads so it can continue listening
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            if (_loadEventChannel != null)
            {
                _loadEventChannel.OnEventRaised += LoadScene;
            }
        }

        private void OnDisable()
        {
            if (_loadEventChannel != null)
            {
                _loadEventChannel.OnEventRaised -= LoadScene;
            }
        }

        private void LoadScene(GameSceneSO sceneToLoad)
        {
            if (sceneToLoad == null) return;

            // Direct asynchronous load
            SceneManager.LoadSceneAsync(sceneToLoad.SceneName);
        }
    }
}
