using UnityEngine;
using Pathfinding;
using KhosaryCode.Events;

namespace KhosaryCode.AI
{
    public class DoctorVisual : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField] private DoctorDeathEventChannelSO onDoctorKnockedOut;

        private float lastInputX = 0f;
        private float lastInputY = -1f; // Default facing down

        private Animator animator;
        private NPCStateMachine stateMachine;
        private AIPath agent;
        private bool isKnockedOut = false;

        private void OnEnable()
        {
            if (onDoctorKnockedOut != null)
            {
                onDoctorKnockedOut.OnEventRaised += OnDoctorKnockedOutRaised;
            }
            else
            {
                Debug.LogWarning($"[DoctorVisual] onDoctorKnockedOut event channel is null on {gameObject.name}!");
            }
        }

        private void OnDisable()
        {
            if (onDoctorKnockedOut != null) onDoctorKnockedOut.OnEventRaised -= OnDoctorKnockedOutRaised;
        }

        private void OnDoctorKnockedOutRaised(DoctorDeathData data)
        {
            Debug.Log($"[DoctorVisual] OnDoctorKnockedOutRaised event received on {gameObject.name}. Payload instance: {(data.Instance != null ? data.Instance.name : "null")}");
            
            if (stateMachine == null)
            {
                Debug.LogError($"[DoctorVisual] stateMachine is null on {gameObject.name}!");
                return;
            }

            bool instanceMatch = data.Instance == stateMachine.gameObject;
            Debug.Log($"[DoctorVisual] Instance match check for {gameObject.name}: {instanceMatch} (Payload: {data.Instance.name} vs Local StateMachine: {stateMachine.gameObject.name})");

            if (instanceMatch)
            {
                PlayHurtAnimation();
            }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            if (animator == null) animator = GetComponentInChildren<Animator>();
            
            stateMachine = GetComponentInParent<NPCStateMachine>();
            agent = GetComponentInParent<AIPath>();

            Debug.Log($"[DoctorVisual] Awake on {gameObject.name}: animator is null? {(animator == null)}, stateMachine is null? {(stateMachine == null)}");
        }

        private void Update()
        {
            if (animator == null || agent == null || stateMachine == null) return;

            // Once knocked out, stop updating locomotion params
            isKnockedOut = stateMachine.CurrentState is KnockedOutStateSO;
            if (isKnockedOut)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
                animator.SetFloat("InputX", 0f);
                animator.SetFloat("InputY", 0f);
                return;
            }

            Vector3 velocity = agent.velocity;
            float speed = velocity.magnitude;
            bool isMoving = speed > 0.1f;

            // Determine if the Doctor is in the fleeing/running state
            bool isRunningState = stateMachine.CurrentState is RunningStateSO;

            animator.SetBool("IsWalking", isMoving && !isRunningState);
            animator.SetBool("IsRunning", isMoving && isRunningState);

            if (isMoving)
            {
                Vector2 dir = velocity.normalized;
                animator.SetFloat("InputX", dir.x);
                animator.SetFloat("InputY", dir.y);

                lastInputX = dir.x;
                lastInputY = dir.y;
            }
            else
            {
                animator.SetFloat("InputX", 0f);
                animator.SetFloat("InputY", 0f);
            }

            animator.SetFloat("LastInputX", lastInputX);
            animator.SetFloat("LastInputY", lastInputY);
        }

        /// <summary>
        /// Called by a VoidEventListener in the Inspector when the doctor is hurt.
        /// </summary>
        public void PlayHurtAnimation(Empty empty)
        {
            PlayHurtAnimation();

        }

        /// <summary>
        /// Fires the Hurt trigger. Safe to call directly from code as well.
        /// </summary>
        public void PlayHurtAnimation()
        {
            //if (animator == null) return;
            animator.SetTrigger("Hurt");
            Debug.Log("Doctor hurt animation triggered.");
        }
    }
}
