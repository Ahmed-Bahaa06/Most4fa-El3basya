using UnityEngine;
using System;
using KhosaryCode.Events;

public class AdrenalinSystem : MonoBehaviour
{

    [Header("Adrenaline Settings")]
    public float currentAdrenaline = 0f;
    public float maxAdrenaline = 100f;
    public float adrenalineDecayRate = 5f;

    [Header("Speed Multiplier Settings")]
    public float baseMoveSpeed = 6f;
    public float maxSpeedMultiplier = 1.8f;

    // [Header("Health Reference")]
    // public PlayerHealth playerHealth;

    [SerializeField] private FloatEventChannelSO OnAdrenalineChanged;


    public float CurrentSpeedMultiplier
    {
        get
        {
            float adrenalineRatio = Mathf.Clamp01(currentAdrenaline / maxAdrenaline);
            return Mathf.Lerp(1.0f, maxSpeedMultiplier, adrenalineRatio);
        }
    }

    private void Start()
    {
        NotifyUI();
    }

    private void Update()
    {
        if (currentAdrenaline > 0f)
        {
            currentAdrenaline -= adrenalineDecayRate * Time.deltaTime;
            currentAdrenaline = Mathf.Max(currentAdrenaline, 0f);
            
            NotifyUI();
        }
    }

    // public void TriggerElectricShock(float adrenalineBoost, float healthDamage)
    // {
    //     currentAdrenaline = Mathf.Min(currentAdrenaline + adrenalineBoost, maxAdrenaline);

    //     if (playerHealth != null)
    //     {
    //         playerHealth.TakeDamage(healthDamage);
    //     }
    //     NotifyUI();
    //     Debug.Log($"[Shock!] Adrenaline boosted to: {currentAdrenaline}.");
    // }

    private void NotifyUI()
    {
        OnAdrenalineChanged?.Invoke(currentAdrenaline);
    }

    public void SetCurrentAdrenaline(float newValue)
    {
        currentAdrenaline = Mathf.Clamp(newValue, 0f, maxAdrenaline);        
        NotifyUI();
    }
    public void IncreaseCurrentAdrenaline(float newValue)
    {
        currentAdrenaline = Mathf.Clamp(currentAdrenaline + newValue, 0f, maxAdrenaline);      
        NotifyUI();
    }
}