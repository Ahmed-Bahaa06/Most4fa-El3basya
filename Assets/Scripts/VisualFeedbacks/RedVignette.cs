using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RedVignette : MonoBehaviour
{
    public Volume volume;
    Vignette vignette;

    public float fadeSpeed = 5f;
    float targetIntensity = 0f;

   
    void Start()
    {
        volume.profile.TryGet(out vignette);
    }

    void Update()
    {
        vignette.intensity.value = Mathf.Lerp(
            vignette.intensity.value,
            targetIntensity,
            Time.deltaTime * fadeSpeed
        );
    }

    public void TakeDamage()
    {
        targetIntensity = 0.285f;
        Invoke(nameof(ResetEffect), 2f);
    }

    void ResetEffect()
    {
        targetIntensity = 0f;
    }
}