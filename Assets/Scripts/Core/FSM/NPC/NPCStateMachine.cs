using UnityEngine;
using Pathfinding;

namespace KhosaryCode.AI
{
    [RequireComponent(typeof(AIPath))]
    public abstract class NPCStateMachine : MonoBehaviour
    {
        public AIPath Agent { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        [Header("References")]
        [SerializeField] private Transform _target; // Exposed for testing
        public Transform Target { get => _target; set => _target = value; }

        public NPCStateSO CurrentState { get; private set; }
        
        // Timers maintained by the state machine so the ScriptableObjects can remain stateless
        public float StateTimer { get; set; } 
        public float ActionTimer { get; set; }

        protected virtual void Awake()
        {
            Agent = GetComponent<AIPath>();
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Initialize(NPCStateSO startingState)
        {
            CurrentState = startingState;
            CurrentState?.OnEnter(this);
        }

        public void ChangeState(NPCStateSO newState)
        {
            if (CurrentState == newState || newState == null) return;

            CurrentState?.OnExit(this);
            CurrentState = newState;
            StateTimer = 0f;
            ActionTimer = 0f;
            CurrentState?.OnEnter(this);
        }

        protected virtual void Update()
        {
            StateTimer += Time.deltaTime;
            CurrentState?.OnUpdate(this);
        }
    }
}
