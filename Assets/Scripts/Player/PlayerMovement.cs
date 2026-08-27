using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (GameInputManager.Instance != null)
            GameInputManager.Instance.OnMove += HandleMove;
    }

    private void OnDisable()
    {
        if (GameInputManager.Instance != null)
            GameInputManager.Instance.OnMove -= HandleMove;
    }

    private void HandleMove(Vector2 input)
    {
        moveInput = input;
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