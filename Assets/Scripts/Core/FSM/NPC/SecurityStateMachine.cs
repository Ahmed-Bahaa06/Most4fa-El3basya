using UnityEngine;
using KhosaryCode.Combat;

namespace KhosaryCode.AI
{
    public class SecurityStateMachine : NPCStateMachine
    {
        [SerializeField] private NPCStateSO _initialState;
        
        public ProjectileWeapon Weapon { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Weapon = GetComponentInChildren<ProjectileWeapon>();
        }

        private void Start()
        {
            if (_initialState != null) 
            {
                Initialize(_initialState);
            }
        }
    }
}
