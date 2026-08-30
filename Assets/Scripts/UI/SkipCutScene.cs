using System.Collections;
using KhosaryCode.Events;
using KhosaryCode.Scenes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class SkipCutScene : MonoBehaviour
{
    [Header("Scene Loading")]
    [SerializeField] private GameSceneSO _cutScene;
    [SerializeField] private LoadEventChannelSO _loadEventChannel;

    [Header("Video Player (Optional)")]
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private float _autoTransitionDelay = 3f;

    private Coroutine _delayedSkipCoroutine;

    private void OnEnable()
    {

        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void OnDisable()
    {

        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached -= OnVideoFinished;
        }

        if (_delayedSkipCoroutine != null)
        {
            StopCoroutine(_delayedSkipCoroutine);
            _delayedSkipCoroutine = null;
        }
    }

    private void Update()
    {
        // Fallback: detect Space or Escape via Keyboard API directly
        // in case the Input System action map is blocked by UI EventSystem
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("[SkipCutScene] Space or Escape key pressed — skipping.");
            SkipCutscene();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log($"[SkipCutScene] Video finished. Transitioning in {_autoTransitionDelay} seconds...");
        if (gameObject.activeInHierarchy && _delayedSkipCoroutine == null)
        {
            _delayedSkipCoroutine = StartCoroutine(DelayedSkip(_autoTransitionDelay));
        }
    }

    private IEnumerator DelayedSkip(float delay)
    {
        yield return new WaitForSeconds(delay);
        _delayedSkipCoroutine = null;
        SkipCutscene();
    }

    public void SkipCutscene()
    {
        // Cancel the delayed transition if manual skip is triggered during the delay
        if (_delayedSkipCoroutine != null)
        {
            StopCoroutine(_delayedSkipCoroutine);
            _delayedSkipCoroutine = null;
        }

        if (_cutScene != null && _loadEventChannel != null)
        {
            _loadEventChannel.Invoke(_cutScene);
        }
        else
        {
            Debug.LogError("[SkipCutScene] Missing scene or load event channel references! Check Inspector.");
        }
    }
}
