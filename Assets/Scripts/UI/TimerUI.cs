using UnityEngine;
using TMPro;

namespace KhosaryCode.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        /// <summary>
        /// Call this method via a FloatEventListener UnityEvent hook.
        /// It formats the raw float seconds into a clean MM:SS string.
        /// </summary>
        /// <param name="timeInSeconds"></param>
        public void UpdateTimeText(float timeInSeconds)
        {
            if (_timerText == null) return;
            
            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
