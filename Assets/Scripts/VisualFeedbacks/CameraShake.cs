using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin noiseComponent;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    [SerializeField] private float intensity;
    [SerializeField] private float time;

    private void Awake() 
    {
        cinemachineCamera = GetComponentInParent<CinemachineCamera>();
        if (cinemachineCamera == null)
        {
            cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        }
        
        if (cinemachineCamera != null)
        {
            noiseComponent = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void ShakeCamera() 
    {
        if (noiseComponent != null)
        {
            noiseComponent.AmplitudeGain = intensity;
        }

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;

        Debug.Log("Camera is shaking");
    }

    private void Update() 
    {
        if (shakeTimer > 0) 
        {
            shakeTimer -= Time.deltaTime;
            
            if (noiseComponent != null)
            {
                if (shakeTimer > 0f)
                {
                    noiseComponent.AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
                }
                else
                {
                    noiseComponent.AmplitudeGain = 0f;
                }
            }
        }
    }
}
