using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float lastDashTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
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
        rb.linearVelocity = moveInput * moveSpeed;
    }
}



