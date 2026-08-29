using UnityEngine;

namespace KhosaryCode.Core.FSM.GameManager.States
{
    [CreateAssetMenu(menuName = "States/Game Win State", fileName = "GameWinState")]
    public class WinStateSO : GameStateSO
    {
        public override void Enter()
        {
            Debug.Log("[WinState] Entered. Player has won.");
            Time.timeScale = 0f;
        }
    }
}
