using UnityEngine;
using UnityEngine.UI;

public class AdrenalinBarUI : MonoBehaviour
{
    [SerializeField] private Slider _adrenalinSlider;
    [SerializeField] private Slider _easeAdrenalinSlider;

    [SerializeField] private float lerpSpeed = 0.01f;

    private void Start()
    {
        AdrenalinSystem system = Object.FindAnyObjectByType<AdrenalinSystem>();
        if (system != null)
        {
            if (_adrenalinSlider != null) _adrenalinSlider.maxValue = system.maxAdrenaline;
            if (_easeAdrenalinSlider != null) _easeAdrenalinSlider.maxValue = system.maxAdrenaline;
        }
    }

    private void Update()
    {
        if (_easeAdrenalinSlider != null)
        {
            _easeAdrenalinSlider.value = Mathf.Lerp(_easeAdrenalinSlider.value, _adrenalinSlider.value, lerpSpeed);
        }
    }

    public void UpdateAdrenalin(float currenteAdrenalin)
    {
        if (_adrenalinSlider != null)
        {
            _adrenalinSlider.value = currenteAdrenalin;
        }
    }
}
