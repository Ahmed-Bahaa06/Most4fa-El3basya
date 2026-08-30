using UnityEngine;
using UnityEngine.EventSystems;
using KhosaryCode.Audio;

namespace KhosaryCode.UI
{
    /// <summary>
    /// Attach this script to any UI Button to automatically play Click and Hover sounds.
    /// It uses the SoundManager to play the sounds (which acts like PlayOneShot but respects your volume sliders and pooling).
    /// </summary>
    public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [Header("Sound Settings")]
        [SerializeField] private bool _playHoverSound = true;
        [SerializeField] private bool _playClickSound = true;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_playHoverSound && SoundManager.Instance != null)
            {
                // Plays the hover sound independently (acts like PlayOneShot through the manager)
                SoundManager.Instance.PlaySound(SoundType.UIButtonHover);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_playClickSound && SoundManager.Instance != null)
            {
                // Plays the click sound independently (acts like PlayOneShot through the manager)
                SoundManager.Instance.PlaySound(SoundType.UIButtonClick);
            }
        }
    }
}
