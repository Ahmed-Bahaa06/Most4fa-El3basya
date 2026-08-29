using UnityEngine;
using KhosaryCode.Events;

namespace KhosaryCode.Core.FSM.GameManager.States
{
    [CreateAssetMenu(menuName = "States/Playing State", fileName = "PlayingState")]
    public class PlayingStateSO : GameStateSO
    {
        [SerializeField] private VoidEventChannelSO _onGameStarted;

        public override void Enter()
        {
            Debug.Log("[PlayingState] Entered. Starting Game...");
            Time.timeScale = 1f;
            if (_onGameStarted != null)
            {
                _onGameStarted.RaiseEvent();
            }
        }
    }
}
