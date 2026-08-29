using UnityEngine;

namespace KhosaryCode.Events
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel", fileName = "New Void Event Channel")]
    public class VoidEventChannelSO : GenericEventChannelSO<Empty>
    {
        public void RaiseEvent()
        {
            Invoke(new Empty());
        }
    }
}
