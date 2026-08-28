using UnityEngine;
using UnityEngine.UI;

namespace KhosaryCode.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        /// <summary>
        /// Call this method via a FloatEventListener UnityEvent hook.
        /// </summary>
        void Awake()
        {
            _healthSlider.value = _healthSlider.maxValue;
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
