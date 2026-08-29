using UnityEngine;
using KhosaryCode.Core.FSM.GameManager;
using KhosaryCode.Events;
using KhosaryCode.AI;
using UnityEngine.InputSystem;

namespace KhosaryCode.Core
{
    [RequireComponent(typeof(GameManagerStateMachine))]
    public class GameManager : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField] private VoidEventChannelSO _onGameStartChannel;
        [SerializeField] private VoidEventChannelSO _onGamePausedChannel;
        [SerializeField] private VoidEventChannelSO _onGameOverChannel;
        [SerializeField] private VoidEventChannelSO _onGameWinChannel;
        [SerializeField] private IntEventChannelSO _onDoctorCountChangedChannel;

        private GameManagerStateMachine _stateMachine;
        private int _totalDoctors;
        private int _remainingDoctors;

        private void Awake()
        {
            _stateMachine = GetComponent<GameManagerStateMachine>();
        }

        private void Start()
        {
            // Initialize Doctors
            DoctorStateMachine[] doctors = FindObjectsByType<DoctorStateMachine>();
            _totalDoctors = doctors.Length;
            _remainingDoctors = _totalDoctors;

            if (_onDoctorCountChangedChannel != null)
            {
                _onDoctorCountChangedChannel.RaiseEvent(_remainingDoctors);
            }

            StartGame();
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
            {
                TogglePause();
            }
        }

        public void HandleDoctorKnockedOut()
        {
            _remainingDoctors--;
            if (_onDoctorCountChangedChannel != null)
            {
                _onDoctorCountChangedChannel.RaiseEvent(_remainingDoctors);
            }

            if (_remainingDoctors <= 0)
            {
                HandleGameWin();
            }
        }

        public void HandleTimeUp()
        {
            _stateMachine.TransitionToComa();
        }

        public void HandlePlayerDead()
        {
            _stateMachine.TransitionToGameOver();
            if (_onGameOverChannel != null)
            {
                _onGameOverChannel.RaiseEvent();
            }
        }

        public void HandleGameWin()
        {
            _stateMachine.TransitionToWin();
            if (_onGameWinChannel != null)
            {
                _onGameWinChannel.RaiseEvent();
            }
        }

        public void StartGame()
        {
            _stateMachine.TransitionToPlaying();
            if (_onGameStartChannel != null)
            {
                _onGameStartChannel.RaiseEvent();
            }
        }

        public void TogglePause()
        {
            _stateMachine.TogglePause();
            if (_onGamePausedChannel != null)
            {
                _onGamePausedChannel.RaiseEvent();
            }
        }
    }
}
