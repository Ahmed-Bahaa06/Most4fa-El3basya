using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using KhosaryCode.Scenes;
using KhosaryCode.Events;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Loading")]
    [SerializeField] private GameSceneSO _nextSceneToLoad;
    [SerializeField] private LoadEventChannelSO _loadEventChannel;

    public void PlayGame()
    {
        if (_nextSceneToLoad != null && _loadEventChannel != null)
        {
            _loadEventChannel.Invoke(_nextSceneToLoad);
        }
        else
        {
            Debug.LogError("[MainMenuUI] Missing scene or load event channel references!");
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!"); 

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unityplayer
        #endif
    }
}