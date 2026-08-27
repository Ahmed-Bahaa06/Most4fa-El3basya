using UnityEngine;
using KhosaryCode.Events;

namespace KhosaryCode.Core.FSM
{
    [CreateAssetMenu(menuName = "States/Game Over State", fileName = "GameOverState")]
    public class GameOverStateSO : GameStateSO
    {
        [SerializeField] private VoidEventChannelSO _onGameEnded;

        public override void Enter()
        {
            Debug.Log("[GameOverState] Entered. Player has died.");
            if (_onGameEnded != null)
            {
                _onGameEnded.RaiseEvent();
            }
        }
    }
}
