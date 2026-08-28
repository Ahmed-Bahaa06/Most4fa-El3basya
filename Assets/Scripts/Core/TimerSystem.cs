using UnityEngine;
using KhosaryCode.Events;

namespace KhosaryCode.Core
{
    public class TimerSystem : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _initialTime = 300f; // Default 5 minutes
        [SerializeField] private bool _autoStart = false;

        [Header("Broadcasting Channels")]
        [SerializeField] private FloatEventChannelSO _onTimeUpdated;
        [SerializeField] private VoidEventChannelSO _onTimeUp;

        private float _currentTime;
        private bool _isRunning;
        private int _lastBroadcastedSecond = -1;

        private void Start()
        {
            _currentTime = _initialTime;
            
            if (_autoStart)
            {
                StartTimer();
            }

            // Broadcast initial time
            if (_onTimeUpdated != null)
            {
                _onTimeUpdated.RaiseEvent(_currentTime);
            }
        }

        public void StartTimer()
        {
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        private void Update()
        {
            if (!_isRunning) return;

            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                
                if (_currentTime <= 0)
                {
                    _currentTime = 0;
                    _isRunning = false;
                    
                    if (_onTimeUp != null)
                    {
                        _onTimeUp.RaiseEvent();
                    }
                }
                
                // PERFORMANCE OPTIMIZATION: 
                // Only broadcast the event when the visual second changes.
                // This prevents the TimerUI from causing String Allocations every single frame!
                int currentSecond = Mathf.CeilToInt(_currentTime);
                if (currentSecond != _lastBroadcastedSecond)
                {
                    _lastBroadcastedSecond = currentSecond;
                    if (_onTimeUpdated != null)
                    {
                        _onTimeUpdated.RaiseEvent(_currentTime);
                    }
                }
            }
        }
    }
}
