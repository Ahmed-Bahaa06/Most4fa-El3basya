using UnityEngine;

namespace KhosaryCode.Events
{
    [CreateAssetMenu(menuName = "Events/Float Event Channel", fileName = "New Float Event Channel")]
    public class FloatEventChannelSO : GenericEventChannelSO<float>
    {
        public void RaiseEvent(float value)
        {
            Invoke(value);
        }
    }
}
