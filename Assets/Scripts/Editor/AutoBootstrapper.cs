#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KhosaryCode.Editor
{
    /// <summary>
    /// This script ensures that if you press Play in the Unity Editor from ANY scene (like Cutscene or Gameplay),
    /// it will quickly load the Bootstrapper scene first so your persistent managers (like SceneLoader) are spawned.
    /// </summary>
    [InitializeOnLoad]
    public static class AutoBootstrapper
    {
        // Change this if your Bootstrapper scene is named differently!
        private const string BOOTSTRAPPER_SCENE_NAME = "BootStrapper";

        static AutoBootstrapper()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                // Remember what scene the developer was actually working on
                EditorPrefs.SetString("LastScenePlayed", SceneManager.GetActiveScene().path);
            }

            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                // If we are already in the Bootstrapper, do nothing
                if (SceneManager.GetActiveScene().name == BOOTSTRAPPER_SCENE_NAME)
                    return;

                // Otherwise, we need to load the Bootstrapper additively to get our managers!
                string[] guids = AssetDatabase.FindAssets($"{BOOTSTRAPPER_SCENE_NAME} t:Scene");
                if (guids.Length > 0)
                {
                    string bootstrapperPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                    Debug.Log($"[AutoBootstrapper] Automatically loading {BOOTSTRAPPER_SCENE_NAME} to spawn managers...");
                    SceneManager.LoadSceneAsync(BOOTSTRAPPER_SCENE_NAME, LoadSceneMode.Additive);
                }
                else
                {
                    Debug.LogWarning($"[AutoBootstrapper] Could not find a scene named {BOOTSTRAPPER_SCENE_NAME}. Persistent managers might not load.");
                }
            }
        }
    }
}
#endif
