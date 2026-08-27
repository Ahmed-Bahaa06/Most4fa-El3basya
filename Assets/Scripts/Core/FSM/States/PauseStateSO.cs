using UnityEngine;

namespace KhosaryCode.Core.FSM
{
    [CreateAssetMenu(menuName = "States/Pause State", fileName = "PauseState")]
    public class PauseStateSO : GameStateSO
    {
        public override void Enter()
        {
            Debug.Log("[PauseState] Entered. Time paused.");
            Time.timeScale = 0f;
        }

        public override void Exit()
        {
            Debug.Log("[PauseState] Exited. Time resumed.");
            Time.timeScale = 1f;
        }
    }
}
