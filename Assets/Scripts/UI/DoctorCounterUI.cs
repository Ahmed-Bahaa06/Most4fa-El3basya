using UnityEngine;
using TMPro;

namespace KhosaryCode.UI
{
    public class DoctorCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private string _prefixText = "Doctors Remaining: ";

        public void UpdateCounter(int remaining)
        {
            if (_counterText != null)
            {
                _counterText.text = _prefixText + remaining;
            }
        }
    }
}
