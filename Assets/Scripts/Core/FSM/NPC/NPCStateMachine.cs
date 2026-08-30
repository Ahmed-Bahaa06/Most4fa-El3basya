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

        protected virtual void Start()
        {
            // If target is null, or is a prefab asset (has no valid scene), or is inactive in the scene:
            if (_target == null || !_target.gameObject.scene.IsValid() || !_target.gameObject.activeInHierarchy)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    _target = player.transform;
                }
            }
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

        [Header("States")]
        [SerializeField] private NPCStateSO _knockedOutStateSO;

        [Header("Audio")]
        [SerializeField] private bool _isFemale = false;
        public bool IsFemale => _isFemale;

        public void PlayHurtSound()
        {
            if (_isFemale)
                KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.FemaleNPCHurt, transform.position);
            else
                KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.MaleNPCHurt, transform.position);
        }

        public void KnockOut()
        {
            if (CurrentState != _knockedOutStateSO && _knockedOutStateSO != null)
            {
                if (_isFemale)
                    KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.FemaleNPCDie, transform.position);
                else
                    KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.MaleNPCDie, transform.position);

                ChangeState(_knockedOutStateSO);
            }
        }

        protected virtual void Update()
        {
            bool isPausedOrDead = (Time.timeScale == 0f) || (CurrentState == _knockedOutStateSO);

            if (Agent != null)
            {
                Agent.isStopped = isPausedOrDead;
                Agent.canMove = !isPausedOrDead;
            }

            if (isPausedOrDead)
            {
                var rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }

                if (SpriteRenderer != null)
                {
                    var anim = GetComponentInChildren<Animator>();
                    if (anim != null)
                    {
                        anim.speed = (Time.timeScale == 0f) ? 0f : 1f;
                    }
                }
                
                if (Time.timeScale == 0f) return;
            }
            else
            {
                if (SpriteRenderer != null)
                {
                    var anim = GetComponentInChildren<Animator>();
                    if (anim != null) anim.speed = 1f;
                }
            }

            StateTimer += Time.deltaTime;
            CurrentState?.OnUpdate(this);
        }
    }
}
