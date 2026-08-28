using UnityEngine;

namespace KhosaryCode.Core.FSM.GameManager
{
    public abstract class GameStateSO : ScriptableObject, IState
    {
        public virtual void Enter() { }
        public virtual void Tick() { }
        public virtual void Exit() { }
    }
}
