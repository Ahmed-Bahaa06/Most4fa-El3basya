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
    [SerializeField] private GameSceneSO _gameplayScene;
    [SerializeField] private LoadEventChannelSO _loadEventChannel;

    public void PlayGame()
    {
        KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.UIButtonClick);
        if (_gameplayScene != null && _loadEventChannel != null)
        {
            _loadEventChannel.Invoke(_gameplayScene);
        }
        else
        {
            Debug.LogError("[MainMenuUI] Missing scene or load event channel references!");
        }
    }
    
    public void QuitGame()
    {
        KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.UIButtonClick);

        Debug.Log("QUIT!"); 
        Application.Quit();
    }
}