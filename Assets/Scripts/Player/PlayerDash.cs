using System.Collections;
using UnityEngine;
using KhosaryCode.AI;
using KhosaryCode.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovement))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashVelocity = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashRecoveryTime = 1f;
    [SerializeField] private Vector2 dashHitboxSize = new Vector2(1f, 1f);
    
    
    [Header("Adrenaline Reward")]
    [SerializeField] private float dashHitAdrenalineReward = 20f;
    
    [Header("References")]
    [SerializeField] private AdrenalinSystem adrenalinSystem;
    [SerializeField] private VoidEventChannelSO OnPlayerDash;
    [SerializeField] private Transform dashVFXPosition;


    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    
    private float lastDashTime;
    
    public bool IsDashing { get; private set; } = false;
    public bool IsRecovering { get; private set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnDash += HandleDash;
        }
    }

    private void OnDisable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnDash -= HandleDash;
        }
    }

    private void HandleDash()
    {
        if (Time.time >= lastDashTime + dashCooldown && !IsDashing && !IsRecovering)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        IsDashing = true;
        OnPlayerDash?.Invoke(new Empty());
        lastDashTime = Time.time;
        
        KhosaryCode.VisualFeedbacks.VFXManager.Instance.PlayVFX(KhosaryCode.VisualFeedbacks.VFXType.PlayerDash, dashVFXPosition.position);
        
        float startTime = Time.time;
        bool hitNPC = false;
        Vector2 direction = playerMovement.FacingDirection;
        
        while (Time.time < startTime + dashDuration)
        {
            rb.linearVelocity = direction * dashVelocity;
            
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rb.position + direction * 0.5f, dashHitboxSize, 0f);
            foreach (Collider2D col in colliders)
            {
                if (col.TryGetComponent(out NPCStateMachine npc))
                {
                    npc.KnockOut();
                    
                    if (adrenalinSystem != null)
                    {
                        adrenalinSystem.IncreaseCurrentAdrenaline(dashHitAdrenalineReward);
                    }
                    
                    KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.PlayerKnockoutHit, transform.position);
                    
                    hitNPC = true;
                    break;
                }
            }
            
            if (hitNPC) break;
            
            yield return new WaitForFixedUpdate();
        }

        rb.linearVelocity = Vector2.zero;
        IsDashing = false;

        if (!hitNPC)
        {
            StartCoroutine(RecoveryRoutine());
        }
    }
    
    private IEnumerator RecoveryRoutine()
    {
        IsRecovering = true;
        yield return new WaitForSeconds(dashRecoveryTime);
        IsRecovering = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying && playerMovement != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + playerMovement.FacingDirection * 0.5f, dashHitboxSize);
        }
    }
}
