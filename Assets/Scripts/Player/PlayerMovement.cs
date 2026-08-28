using UnityEngine;
using KhosaryCode.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float currentMoveSpeed;  
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private AdrenalinSystem adrenalinSystem;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float lastDashTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = baseMoveSpeed;
    }

    private void OnEnable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnMove += HandleMove;
            GameInputManager.Instance.OnDash += HandleDash;
            GameInputManager.Instance.OnInteract += HandleInteract;
            
        }
    }

    private void OnDisable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnMove -= HandleMove;
            GameInputManager.Instance.OnDash -= HandleDash;
            GameInputManager.Instance.OnInteract -= HandleInteract;
            
        }
    }

    private void HandleMove(Vector2 input)
    {
        moveInput = input;
    }

    private void HandleDash()
    {
        if (Time.time >= lastDashTime + dashCooldown)
        {
            if (moveInput != Vector2.zero)
            {
                Vector3 dashDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
                rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);
                lastDashTime = Time.time;
            }
        }
    }

    private void HandleInteract()
    {
        float interactRange = 2f;
        // استخدام Physics2D عشان المشروع 2D
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        currentMoveSpeed = baseMoveSpeed * adrenalinSystem.CurrentSpeedMultiplier;
        rb.linearVelocity = moveInput * currentMoveSpeed;
    }
}