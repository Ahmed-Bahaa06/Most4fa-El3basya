using System;
using KhosaryCode.Events;
using UnityEngine;

public class PlayerHealth : MonoBehaviour , IDamagable
{
    [Header("Health Settings")]
    [SerializeField] private Health health;
    [SerializeField] private float maxHealth;
    [SerializeField] private FloatEventChannelSO OnTakeDamage;
    [SerializeField] private VoidEventChannelSO OnDie;




    private void Start()
    {
        health = new Health(maxHealth);
        
    }

    public void TakeDamage(float damageAmount)
    {
        health.Damage(damageAmount);
        Debug.Log("Current Health is: " + health.GetHealth());
    
        OnTakeDamage?.Invoke(health.GetHealth());

        if (health.GetHealth() <= 0)
        {
            Die();
            Debug.Log("Player died");
        }
    }

    private void Die()
    {
          OnDie?.Invoke(new Empty());
    }
}
