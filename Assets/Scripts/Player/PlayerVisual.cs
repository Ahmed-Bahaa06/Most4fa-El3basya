using UnityEngine;
using KhosaryCode.Events;

public class PlayerVisual : MonoBehaviour
{
    private float lastInputX;
    private float lastInputY = -1f; // Default facing down

    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerDash = GetComponentInParent<PlayerDash>();
    }

    private void Update()
    {
        if (animator == null || playerMovement == null || isDead) return;

        bool isDashing = playerDash != null && playerDash.IsDashing;
        bool isRecovering = playerDash != null && playerDash.IsRecovering;

        // If the player is dashing or recovering, we freeze movement/turning inputs
        // to let the dash and recovery visuals play out cleanly.
        if (isDashing || isRecovering)
        {
            animator.SetBool("IsRunning", false);
            animator.SetFloat("InputX", 0f);
            animator.SetFloat("InputY", 0f);

            // Keep updating facing direction so visual matches dash direction
            lastInputX = playerMovement.FacingDirection.x;
            lastInputY = playerMovement.FacingDirection.y;
            animator.SetFloat("LastInputX", lastInputX);
            animator.SetFloat("LastInputY", lastInputY);

            // Dash plays at its natural speed
            animator.speed = 1f;
            return;
        }

        Vector2 moveInput = playerMovement.MoveInput;
        bool isMoving = playerMovement.IsMoving;

        animator.SetBool("IsRunning", isMoving);
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        if (isMoving)
        {
            lastInputX = moveInput.x;
            lastInputY = moveInput.y;

            // Scale the entire animation speed to match the player's current speed ratio.
            // At base speed: ratio = 1.0 (normal speed).
            // With adrenaline boost: ratio > 1.0 (faster animation).
            animator.speed = playerMovement.CurrentSpeedRatio;
        }
        else
        {
            // Sync with PlayerMovement's facing direction when idle
            lastInputX = playerMovement.FacingDirection.x;
            lastInputY = playerMovement.FacingDirection.y;

            // Reset to normal speed when idle so the idle animation isn't affected
            animator.speed = 1f;
        }

        animator.SetFloat("LastInputX", lastInputX);
        animator.SetFloat("LastInputY", lastInputY);
    }

    // Public API for Event Listeners (Zero Code Coupling)

    /// <summary>
    /// Triggers the dash (Thrust) animation.
    /// </summary>
    public void PlayDashAnimation()
    {
        if (animator != null && !isDead)
        {
            // Update facing direction immediately before playing animation
            if (playerMovement != null)
            {
                lastInputX = playerMovement.FacingDirection.x;
                lastInputY = playerMovement.FacingDirection.y;
                animator.SetFloat("LastInputX", lastInputX);
                animator.SetFloat("LastInputY", lastInputY);
            }

            // Instantly play the dash animation, bypassing transition settings
            animator.Play("Trust_Tree", 0, 0f);
        }
    }

    /// <summary>
    /// Overload for VoidEventChannelSO listeners.
    /// </summary>
    public void PlayDashAnimation(Empty empty)
    {
        PlayDashAnimation();
    }

    /// <summary>
    /// Triggers the Hurt animation.
    /// </summary>
    public void PlayHurtAnimation(float damageAmount)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
    }

    /// <summary>
    /// Triggers the Die animation.
    /// </summary>
    public void PlayDieAnimation()
    {
        if (isDead) return;
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }

    /// <summary>
    /// Overload for VoidEventChannelSO listeners.
    /// </summary>
    public void PlayDieAnimation(Empty empty)
    {
        PlayDieAnimation();
    }
}
