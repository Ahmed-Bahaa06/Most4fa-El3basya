using UnityEngine;

namespace KhosaryCode.Core.FSM
{
    public class GameManagerStateMachine : StateMachine
    {
        [Header("State Setup")]
        [SerializeField] private GameStateSO _initialState;
        
        [Header("Available States")]
        [SerializeField] private GameStateSO _playingState;
        [SerializeField] private GameStateSO _pauseState;
        [SerializeField] private GameStateSO _gameOverState;
        [SerializeField] private GameStateSO _comaState;

        private void Start()
        {
            if (_initialState != null)
            {
                Initialize(_initialState);
            }
        }

        public void TransitionToComa()
        {
            if (CurrentState == _playingState)
            {
                ChangeState(_comaState);
            }
        }

        public void TransitionToGameOver()
        {
            if (CurrentState == _playingState)
            {
                ChangeState(_gameOverState);
            }
        }

        public void TransitionToPlaying()
        {
            ChangeState(_playingState);
        }

        public void TogglePause()
        {
            if (CurrentState == _playingState)
            {
                ChangeState(_pauseState);
            }
            else if (CurrentState == _pauseState)
            {
                ChangeState(_playingState);
            }
        }
    }
}
