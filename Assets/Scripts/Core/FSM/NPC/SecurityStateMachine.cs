using UnityEngine;

namespace KhosaryCode.AI
{
    public class SecurityStateMachine : NPCStateMachine
    {
        [SerializeField] private NPCStateSO _initialState;

        private void Start()
        {
            if (_initialState != null) 
            {
                Initialize(_initialState);
            }
        }
    }
}
