using UnityEngine;
using UnityEngine.UI;

namespace KhosaryCode.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _easeHealthSlider;

        [SerializeField] private float lerpSpeed = 0.05f;

        /// <summary>
        /// Call this method via a FloatEventListener UnityEvent hook.
        /// </summary>
        void Awake()
        {
            _healthSlider.value = _healthSlider.maxValue;
        }

        private void Update()
        {
            if (_easeHealthSlider != null)
            {
                _easeHealthSlider.value = Mathf.Lerp(_easeHealthSlider.value, _healthSlider.value, lerpSpeed);
            }
        }

        public void DecreaseHealth(float value)
        {
            if (_healthSlider != null)
            {
                _healthSlider.value -= value;
            }
        }
    }
}
