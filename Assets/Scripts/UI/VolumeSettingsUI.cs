using UnityEngine;
using UnityEngine.UI;
using KhosaryCode.Audio;

namespace KhosaryCode.UI
{
    public class VolumeSettingsUI : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField] private Slider _backgroundVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        private void Start()
        {
            // Initialize sliders to match the SoundManager's current values
            if (SoundManager.Instance != null)
            {
                if (_backgroundVolumeSlider != null)
                {
                    _backgroundVolumeSlider.value = SoundManager.Instance.BackgroundVolume;
                    _backgroundVolumeSlider.onValueChanged.AddListener(OnBackgroundVolumeChanged);
                }

                if (_sfxVolumeSlider != null)
                {
                    _sfxVolumeSlider.value = SoundManager.Instance.SFXVolume;
                    _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
                }
            }
            else
            {
                Debug.LogWarning("[VolumeSettingsUI] SoundManager not found in scene!");
            }
        }

        private void OnBackgroundVolumeChanged(float value)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SetBackgroundVolume(value);
            }
        }

        private void OnSFXVolumeChanged(float value)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SetSFXVolume(value);
            }
        }
    }
}
