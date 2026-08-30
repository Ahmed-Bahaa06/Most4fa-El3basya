using UnityEngine;
using KhosaryCode.Events;
using KhosaryCode.Scenes;

namespace KhosaryCode.Cutscenes
{
    /// <summary>
    /// Handles transitioning from a Cutscene to the next gameplay scene.
    /// You can call FinishCutscene() from a Timeline signal, an Animation Event, or user input (skip button).
    /// </summary>
    public class CutsceneManager : MonoBehaviour
    {
        [Header("Scene Transition")]
        [SerializeField] private GameSceneSO _nextSceneToLoad;
        [SerializeField] private LoadEventChannelSO _loadEventChannel;

        [Header("Settings")]
        [SerializeField, Tooltip("Allow the player to skip the cutscene by pressing Space?")]
        private bool _allowSkip = true;

        private void Update()
        {
            if (_allowSkip && Input.GetKeyDown(KeyCode.Space))
            {
                FinishCutscene();
            }
        }

        /// <summary>
        /// Call this when the Cutscene is complete.
        /// </summary>
        public void FinishCutscene()
        {
            if (_nextSceneToLoad != null && _loadEventChannel != null)
            {
                _loadEventChannel.Invoke(_nextSceneToLoad);
            }
            else
            {
                Debug.LogError("[CutsceneManager] Missing Scene or Event Channel references!");
            }
        }
    }
}
