using UnityEngine;
using KhosaryCode.Events;

namespace KhosaryCode.Core.FSM.GameManager.States
{
    [CreateAssetMenu(menuName = "States/Game Over State", fileName = "GameOverState")]
    public class GameOverStateSO : GameStateSO
    {
        [SerializeField] private VoidEventChannelSO _onPlayerDie;

        public override void Enter()
        {
            Debug.Log("[GameOverState] Entered. Player has died.");
            if (_onPlayerDie != null)
            {
                _onPlayerDie.RaiseEvent();
            }
        }
    }
}
