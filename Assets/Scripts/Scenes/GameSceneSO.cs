using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KhosaryCode.Scenes
{
    /// <summary>
    /// ScriptableObject representing a Scene in the game.
    /// Allows assigning scenes in the inspector without relying on Build Indices.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGameScene", menuName = "KhosaryCode/Scenes/Game Scene")]
    public class GameSceneSO : ScriptableObject
    {
#if UNITY_EDITOR
        [Header("Editor Only")]
        [SerializeField, Tooltip("Drag the Scene asset here.")]
        private SceneAsset _sceneAsset;
#endif

        [Header("Runtime Info")]
        [SerializeField, Tooltip("The name of the scene (auto-filled).")]
        private string _sceneName;

        /// <summary>
        /// The name of the scene to load.
        /// </summary>
        public string SceneName => _sceneName;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_sceneAsset != null)
            {
                _sceneName = _sceneAsset.name;
            }
            else
            {
                _sceneName = string.Empty;
            }
        }
#endif
    }
}
