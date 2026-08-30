using UnityEngine;

namespace KhosaryCode.Events
{
    [CreateAssetMenu(fileName = "OfficerDeathEventChannel", menuName = "Events/Officer Death Event Channel")]
    public class OfficerDeathEventChannelSO : GenericEventChannelSO<OfficerDeathData>
    {
        public void RaiseEvent(GameObject instance)
        {
            Invoke(new OfficerDeathData { Instance = instance });
        }
    }
}
