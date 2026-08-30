using KhosaryCode.Events;
using UnityEngine;

public class ElectricWire : MonoBehaviour,IInteractable
{
    [Header("Wire Shock Settings")]
    [SerializeField] private float adrenalineBoost = 40f;
    [SerializeField] private float healthDamage = 15f;

    [SerializeField] private FloatEventChannelSO _onAdrenalinIncrease;

    public void Interact()
    {
        ApplyShock();
    }

    private void ApplyShock()
     {
        KhosaryCode.VisualFeedbacks.VFXManager.Instance.PlayVFX(KhosaryCode.VisualFeedbacks.VFXType.ElectricWireShock, transform.position);
        _onAdrenalinIncrease?.Invoke(adrenalineBoost);
        
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(healthDamage);
        }
        else
        {
            Debug.LogWarning("[ElectricWire] PlayerHealth not found!");
        }
        Destroy(gameObject);
    }
}

