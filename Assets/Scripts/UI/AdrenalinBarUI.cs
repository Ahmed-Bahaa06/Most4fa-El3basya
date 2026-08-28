using UnityEngine;
using UnityEngine.UI;

public class AdrenalinBarUI : MonoBehaviour
{
     [SerializeField] private Slider _adrenalinSlider; 

     public void UpdateAdrenalin(float currenteAdrenalin)
        {
            if (_adrenalinSlider != null)
            {
                    _adrenalinSlider.value = currenteAdrenalin;
            }
        }
}
