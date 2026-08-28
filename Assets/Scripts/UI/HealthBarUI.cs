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
        public void UpdateHealth(float currentHealth)
        {
            if (_healthSlider != null)
            {
                _healthSlider.value = currentHealth;
            }
        }
    }
}
