using UnityEngine;
using UnityEngine.UI;

namespace KhosaryCode.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _easeHealthSlider;

        [SerializeField] private float lerpSpeed = 0.05f;

        private void Start()
        {
            // Auto-sync maxValue from the PlayerHealth component in the scene
            PlayerHealth playerHealth = Object.FindAnyObjectByType<PlayerHealth>();
            if (playerHealth != null)
            {
                float maxHealth = playerHealth.MaxHealth;
                if (_healthSlider != null)
                {
                    _healthSlider.maxValue = maxHealth;
                    _healthSlider.value = maxHealth;
                }
                if (_easeHealthSlider != null)
                {
                    _easeHealthSlider.maxValue = maxHealth;
                    _easeHealthSlider.value = maxHealth;
                }
            }
            else
            {
                Debug.LogWarning("[HealthBarUI] Could not find PlayerHealth in scene. Make sure maxValue is set manually on the sliders.");
            }
        }

        private void Update()
        {
            if (_easeHealthSlider != null)
            {
                _easeHealthSlider.value = Mathf.Lerp(_easeHealthSlider.value, _healthSlider.value, lerpSpeed);
            }
        }

        /// <summary>
        /// Called via FloatEventListener when the player takes damage.
        /// Receives the exact current health and sets the slider to it.
        /// </summary>
        public void DecreaseHealth(float currentHealth)
        {
            if (_healthSlider != null)
            {
                _healthSlider.value = currentHealth;
            }
        }
    }
}
