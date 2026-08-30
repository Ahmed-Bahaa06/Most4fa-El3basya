using UnityEngine;

namespace KhosaryCode.Events
{
    /// <summary>
    /// A simple empty struct used to represent a void/no-parameter event.
    /// </summary>
    [System.Serializable]
    public struct Empty { }
    public struct DoctorDeathData
    {
        public GameObject Instance;
    }
    public struct OfficerDeathData
    {
        public GameObject Instance;
    }
}
