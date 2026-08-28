using KhosaryCode.Events;
using UnityEngine;

public class ElectricWire : MonoBehaviour,IInteractable
{
    [Header("Wire Shock Settings")]
    [SerializeField] private float adrenalineBoost = 40f;
    [SerializeField] private float healthDamage = 15f;

    [Header("Interaction Type")]
    [SerializeField] private bool pickupOnTouch = true;

    [SerializeField] private FloatEventChannelSO _onPlayerDamaged;
    [SerializeField] private FloatEventChannelSO _onAdrenalinIncrease;

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (pickupOnTouch && collision.CompareTag("Player"))
    //     {
    //         ApplyShock();
    //     }
    // }

    public void Interact()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            ApplyShock();
        }
    }

    private void ApplyShock()
     {
    //     AdrenalinSystem adrenalinSystem = player.GetComponent<AdrenalinSystem>();

    //     if (adrenalinSystem != null)
    //     {
    //         adrenalinSystem.TriggerElectricShock(adrenalineBoost, healthDamage);
    //     }

        _onPlayerDamaged?.Invoke(healthDamage);
        _onAdrenalinIncrease?.Invoke(adrenalineBoost);
        Destroy(gameObject);
    }
}

