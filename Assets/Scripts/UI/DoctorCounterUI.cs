using UnityEngine;
using TMPro;

namespace KhosaryCode.UI
{
    public class DoctorCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        public void UpdateCounter(int remainingDoctors)
        {
            if (_counterText != null)
            {
                _counterText.text = "Doctors Remaining: " + remainingDoctors;
            }
        }
    }
}
