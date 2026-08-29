using UnityEngine;

namespace KhosaryCode.Events
{
    /// <summary>
    /// Event channel for requesting scene loads.
    /// Takes a GameSceneSO as the parameter to identify which scene to load.
    /// </summary>
    [CreateAssetMenu(fileName = "LoadEventChannel", menuName = "KhosaryCode/Events/Load Event Channel")]
    public class LoadEventChannelSO : GenericEventChannelSO<KhosaryCode.Scenes.GameSceneSO>
    {
    }
}
