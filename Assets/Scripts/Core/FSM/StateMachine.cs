using UnityEngine;

namespace KhosaryCode.Core.FSM
{
    /// <summary>
    /// Base MonoBehaviour State Machine. All entity state machines (Enemy, GameManager, etc.) will inherit from this.
    /// </summary>
    public abstract class StateMachine : MonoBehaviour
    {
        public IState CurrentState { get; private set; }

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            CurrentState?.Enter();
        }

        public void ChangeState(IState newState)
        {
            if (CurrentState == newState) return;
            
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        protected virtual void Update()
        {
            CurrentState?.Tick();
        }
    }
}
