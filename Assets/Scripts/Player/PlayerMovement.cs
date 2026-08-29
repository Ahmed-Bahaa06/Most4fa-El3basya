using UnityEngine;
using KhosaryCode.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float currentMoveSpeed;  
    
    [Header("References")]
    [SerializeField] private AdrenalinSystem adrenalinSystem;
    [SerializeField] private LayerMask wallLayerMask;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Vector2 MoveInput => moveInput;
    public bool IsMoving => moveInput.sqrMagnitude > 0.01f;
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    /// <summary>Ratio of current speed to base speed (1.0 = normal, > 1.0 = adrenaline boosted).</summary>
    public float CurrentSpeedRatio => baseMoveSpeed > 0f ? currentMoveSpeed / baseMoveSpeed : 1f;
    
    private PlayerDash playerDash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
        currentMoveSpeed = baseMoveSpeed;

        // Auto-assign default wall LayerMask to "Obstacles" or "Walls" if left unassigned
        if (wallLayerMask == 0)
        {
            int obstaclesLayer = LayerMask.NameToLayer("Obstacles");
            if (obstaclesLayer == -1) obstaclesLayer = LayerMask.NameToLayer("Walls");
            if (obstaclesLayer != -1)
            {
                wallLayerMask = 1 << obstaclesLayer;
            }
        }
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

    private void Update()
    {
        bool canTurn = true;
        if (playerDash != null && (playerDash.IsDashing || playerDash.IsRecovering))
        {
            canTurn = false;
        }

        if (moveInput.sqrMagnitude > 0.01f && canTurn)
        {
            UpdateFacingDirection(moveInput);
        }
    }

    private void UpdateFacingDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            FacingDirection = input.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            FacingDirection = input.y > 0 ? Vector2.up : Vector2.down;
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
            UpdateFacingDirection(moveInput);
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

        Vector2 correctedInput = moveInput;

        // Perform wall check and velocity correction only when moving diagonally
        if (Mathf.Abs(moveInput.x) > 0.01f && Mathf.Abs(moveInput.y) > 0.01f)
        {
            bool hittingHorizontalWall = IsHittingWall(new Vector2(Mathf.Sign(moveInput.x), 0f));
            bool hittingVerticalWall = IsHittingWall(new Vector2(0f, Mathf.Sign(moveInput.y)));

            if (hittingHorizontalWall)
            {
                correctedInput.x = 0f;
            }
            if (hittingVerticalWall)
            {
                correctedInput.y = 0f;
            }

            if (correctedInput.sqrMagnitude > 0.01f)
            {
                correctedInput = correctedInput.normalized;
            }
        }

        rb.linearVelocity = correctedInput * currentMoveSpeed;
    }

    private bool IsHittingWall(Vector2 direction)
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(wallLayerMask);
            filter.useLayerMask = true;
            filter.useTriggers = false;

            RaycastHit2D[] results = new RaycastHit2D[1];
            int hits = col.Cast(direction, filter, results, 0.05f); // Cast a tiny distance of 0.05 units
            return hits > 0;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, wallLayerMask);
            return hit.collider != null;
        }
    }
}