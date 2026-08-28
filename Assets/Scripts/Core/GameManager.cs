using UnityEngine;
using KhosaryCode.Core.FSM.GameManager;

namespace KhosaryCode.Core
{
    [RequireComponent(typeof(GameManagerStateMachine))]
    public class GameManager : MonoBehaviour
    {
        private GameManagerStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = GetComponent<GameManagerStateMachine>();
        }

        public void HandleTimeUp()
        {
            _stateMachine.TransitionToComa();
        }

        public void HandlePlayerDead()
        {
            _stateMachine.TransitionToGameOver();
        }

        public void StartGame()
        {
            _stateMachine.TransitionToPlaying();
        }

        public void TogglePause()
        {
            _stateMachine.TogglePause();
        }
    }
}
