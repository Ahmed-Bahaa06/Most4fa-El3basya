using UnityEngine;
using System.Collections;

public class DamageVignetteUI : MonoBehaviour
{


    [Header("UI Reference")]
    [SerializeField] private CanvasGroup vignetteCanvasGroup;
    
    [Header("Vignette Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float maxAlpha = 1f;        
    [SerializeField] private float punchDuration = 0.05f;  
    [SerializeField] private float fadeOutSpeed = 2f;    

    private Coroutine currentFadeRoutine;
    
    private void Awake()
    {
        if (vignetteCanvasGroup != null)
        {
           vignetteCanvasGroup.alpha = 0f;
        }
    }
  

    public void TriggerDamageEffect()
    {
        if (vignetteCanvasGroup == null) return;

        if (currentFadeRoutine != null)
        {
            StopCoroutine(currentFadeRoutine);
        }
        currentFadeRoutine = StartCoroutine(FadeVignetteRoutine());
    }

    private IEnumerator FadeVignetteRoutine()
    {
        vignetteCanvasGroup.alpha = maxAlpha;
        yield return new WaitForSeconds(punchDuration);

        while (vignetteCanvasGroup.alpha > 0f)
        {
            vignetteCanvasGroup.alpha -= fadeOutSpeed * Time.deltaTime;
            yield return null;
        }

        vignetteCanvasGroup.alpha = 0f;
    }
}
