using KhosaryCode.Events;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private FloatEventChannelSO OnPlayerDamaged;


    public void UpdateHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
