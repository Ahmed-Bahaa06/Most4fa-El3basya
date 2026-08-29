using KhosaryCode.Events;
using UnityEngine;

public class ElectricWire : MonoBehaviour,IInteractable
{
    [Header("Wire Shock Settings")]
    [SerializeField] private float adrenalineBoost = 40f;
    [SerializeField] private float healthDamage = 15f;

    [SerializeField] private FloatEventChannelSO _onAdrenalinIncrease;
    [SerializeField] private FloatEventChannelSO _onPlayerDamage;

    public void Interact()
    {
        ApplyShock();
    }

    private void ApplyShock()
     {
        _onAdrenalinIncrease?.Invoke(adrenalineBoost);
        _onPlayerDamage?.Invoke(healthDamage);
        Destroy(gameObject);
    }
}

