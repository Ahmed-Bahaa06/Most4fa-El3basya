using UnityEngine;

namespace KhosaryCode.Events
{
    [CreateAssetMenu(fileName = "DoctorDeathEventChannel", menuName = "Events/Doctor Death Event Channel")]
    public class DoctorDeathEventChannelSO : GenericEventChannelSO<DoctorDeathData>
    {
        public void RaiseEvent(GameObject instance)
        {
            Invoke(new DoctorDeathData { Instance = instance });
        }
    }
}