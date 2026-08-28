using UnityEngine;

public class ElectricWire : MonoBehaviour,IInteractable
{
    [Header("Wire Shock Settings")]
    [SerializeField] private float adrenalineBoost = 40f;
    [SerializeField] private float healthDamage = 15f;

    [Header("Interaction Type")]
    [SerializeField] private bool pickupOnTouch = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickupOnTouch && collision.CompareTag("Player"))
        {
            ApplyShock(collision.gameObject);
        }
    }

    public void Interact()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            ApplyShock(player);
        }
    }

    private void ApplyShock(GameObject player)
    {
        AdrenalinSystem adrenalinSystem = player.GetComponent<AdrenalinSystem>();

        if (adrenalinSystem != null)
        {
            adrenalinSystem.TriggerElectricShock(adrenalineBoost, healthDamage);
        }

        Destroy(gameObject);
    }
}

