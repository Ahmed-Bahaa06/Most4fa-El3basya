using UnityEngine;

namespace KhosaryCode.Events
{
    [CreateAssetMenu(menuName = "Events/Int Event Channel", fileName = "New Int Event Channel")]
    public class IntEventChannelSO : GenericEventChannelSO<int>
    {
        public void RaiseEvent(int value)
        {
            Invoke(value);
        }
    }
}
