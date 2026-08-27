using UnityEngine;
using KhosaryCode.Events;

namespace KhosaryCode.Core.FSM
{
    [CreateAssetMenu(menuName = "States/Coma State", fileName = "ComaState")]
    public class ComaStateSO : GameStateSO
    {
        [SerializeField] private VoidEventChannelSO _onGameEnded;

        public override void Enter()
        {
            Debug.Log("[ComaState] Entered. Time is up, player passed out.");
            if (_onGameEnded != null)
            {
                _onGameEnded.RaiseEvent();
            }
        }
    }
}
