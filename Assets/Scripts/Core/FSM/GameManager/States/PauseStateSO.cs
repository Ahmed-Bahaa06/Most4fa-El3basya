using UnityEngine;

namespace KhosaryCode.Core.FSM.GameManager.States
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
