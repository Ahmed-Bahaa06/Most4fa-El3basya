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

    public float MaxHealth => maxHealth;

    private void Start()
    {
        health = new Health(maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        health.Damage(damageAmount);
        Debug.Log("Current Health is: " + health.GetHealth());
    
        // Pass the CURRENT health to the UI, not the damage amount, to prevent desyncs!
        OnTakeDamage?.Invoke(health.GetHealth());

        if (health.GetHealth() <= 0)
        {
            Die();
            Debug.Log("Player died");
        }
        else
        {
            KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.PlayerHurt, transform.position);
        }
    }

    private void Die()
    {
        KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.PlayerDie, transform.position);
        OnDie?.Invoke(new Empty());
    }
}
