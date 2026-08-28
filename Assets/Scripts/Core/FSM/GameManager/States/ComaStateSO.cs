using UnityEngine;
using KhosaryCode.Events;

namespace KhosaryCode.Core.FSM.GameManager.States
{
    [CreateAssetMenu(menuName = "States/Coma State", fileName = "ComaState")]
    public class ComaStateSO : GameStateSO
    {
        [SerializeField] private VoidEventChannelSO _onTimeEnded;

        public override void Enter()
        {
            Debug.Log("[ComaState] Entered. Time is up, player passed out.");
            if (_onTimeEnded != null)
            {
                _onTimeEnded.RaiseEvent();
            }
        }
    }
}
