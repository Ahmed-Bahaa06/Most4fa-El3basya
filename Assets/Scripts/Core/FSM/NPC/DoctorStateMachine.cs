using UnityEngine;

namespace KhosaryCode.AI
{
    public class DoctorStateMachine : NPCStateMachine
    {
        [SerializeField] private NPCStateSO _initialState;

        protected override void Start()
        {
            base.Start();
            if (_initialState != null) 
            {
                Initialize(_initialState);
            }
        }
    }
}
