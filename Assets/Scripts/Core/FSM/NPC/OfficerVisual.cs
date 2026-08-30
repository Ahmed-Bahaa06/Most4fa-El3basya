using UnityEngine;
using Pathfinding;
using KhosaryCode.Events;

namespace KhosaryCode.AI
{
    /// <summary>
    /// Drives the Animator for both Officer variants (Melee and Gun).
    /// Attach to the visual child that has the Animator. Set the attack trigger
    /// name to "Slash" for melee officers or "Shoot" for gun officers.
    /// </summary>
    public class OfficerVisual : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField] private OfficerDeathEventChannelSO onOfficerKnockedOut;

        [Header("Attack")]
        [Tooltip("The Animator trigger to fire when this officer attacks. Use 'Slash' for melee, 'Shoot' for gun.")]
        [SerializeField] private string _attackTriggerName = "Slash";

        private float lastInputX = 0f;
        private float lastInputY = -1f; // Default facing down

        private Animator animator;
        private NPCStateMachine stateMachine;
        private AIPath agent;
        private bool isKnockedOut = false;

        private void OnEnable()
        {
            if (onOfficerKnockedOut != null)
            {
                onOfficerKnockedOut.OnEventRaised += OnOfficerKnockedOutRaised;
            }
            else
            {
                Debug.LogWarning($"[OfficerVisual] onOfficerKnockedOut event channel is null on {gameObject.name}!");
            }
        }

        private void OnDisable()
        {
            if (onOfficerKnockedOut != null) onOfficerKnockedOut.OnEventRaised -= OnOfficerKnockedOutRaised;
        }

        private void OnOfficerKnockedOutRaised(OfficerDeathData data)
        {
            Debug.Log($"[OfficerVisual] OnOfficerKnockedOutRaised event received on {gameObject.name}. Payload instance: {(data.Instance != null ? data.Instance.name : "null")}");

            if (stateMachine == null)
            {
                Debug.LogError($"[OfficerVisual] stateMachine is null on {gameObject.name}!");
                return;
            }

            bool instanceMatch = data.Instance == stateMachine.gameObject;
            Debug.Log($"[OfficerVisual] Instance match check for {gameObject.name}: {instanceMatch} (Payload: {data.Instance.name} vs Local StateMachine: {stateMachine.gameObject.name})");

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

            Debug.Log($"[OfficerVisual] Awake on {gameObject.name}: animator={animator != null}, stateMachine={stateMachine != null}, attackTrigger={_attackTriggerName}");
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

            // Chasing or running = IsRunning; patrolling = IsWalking
            bool isRunningState = stateMachine.CurrentState is RunningStateSO
                                  || stateMachine.CurrentState is SecurityChaseStateSO
                                  || stateMachine.CurrentState is ShootingStateSO;

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
        /// Fires the attack animation trigger (Slash for melee, Shoot for gun).
        /// Called from SecurityChaseStateSO or ShootingStateSO when attacking.
        /// </summary>
        public void PlayAttackAnimation()
        {
            if (animator == null || string.IsNullOrEmpty(_attackTriggerName)) return;
            animator.SetTrigger(_attackTriggerName);
            Debug.Log($"[OfficerVisual] Attack animation triggered: {_attackTriggerName} on {gameObject.name}");
        }

        /// <summary>
        /// Fires the Hurt trigger. Called on knockout via event channel.
        /// </summary>
        public void PlayHurtAnimation()
        {
            if (animator == null) return;
            animator.SetTrigger("Hurt");
            Debug.Log($"[OfficerVisual] Hurt animation triggered on {gameObject.name}");
        }

        /// <summary>
        /// Overload for VoidEventListener compatibility.
        /// </summary>
        public void PlayHurtAnimation(Empty empty) => PlayHurtAnimation();
    }
}
