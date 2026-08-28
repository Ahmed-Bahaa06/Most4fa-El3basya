using UnityEngine;
using KhosaryCode.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float currentMoveSpeed;  
    
    [Header("References")]
    [SerializeField] private AdrenalinSystem adrenalinSystem;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    
    private PlayerDash playerDash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        currentMoveSpeed = baseMoveSpeed;
    }

    private void OnEnable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnMove += HandleMove;
            GameInputManager.Instance.OnInteract += HandleInteract;
        }
    }

    private void OnDisable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnMove -= HandleMove;
            GameInputManager.Instance.OnInteract -= HandleInteract;
        }
    }

    private void HandleMove(Vector2 input)
    {
        moveInput = input;
        
        // Don't change facing if we are currently dashing or recovering
        bool canTurn = true;
        if (playerDash != null && (playerDash.IsDashing || playerDash.IsRecovering))
        {
            canTurn = false;
        }

        if (moveInput.sqrMagnitude > 0.01f && canTurn)
        {
            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            {
                FacingDirection = moveInput.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                FacingDirection = moveInput.y > 0 ? Vector2.up : Vector2.down;
            }
            
            if (animator != null)
            {
                animator.SetFloat("DirX", FacingDirection.x);
                animator.SetFloat("DirY", FacingDirection.y);
            }
        }
    }

    private void HandleInteract()
    {
        if (playerDash != null && (playerDash.IsDashing || playerDash.IsRecovering)) return;
        
        float interactRange = 2f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D col in colliders)
        {
            if (col.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        bool canMove = true;
        if (playerDash != null && (playerDash.IsDashing || playerDash.IsRecovering))
        {
            canMove = false;
        }

        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        if (adrenalinSystem != null)
        {
            currentMoveSpeed = baseMoveSpeed * adrenalinSystem.CurrentSpeedMultiplier;
        }
        rb.linearVelocity = moveInput * currentMoveSpeed;
    }
}