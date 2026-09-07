using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin noiseComponent;

    [Header("Settings")]
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float frequency = 2f;
    [SerializeField] private float time = 0.3f;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake() 
    {
        if (cinemachineCamera == null)
            cinemachineCamera = GetComponent<CinemachineCamera>();

        if (noiseComponent == null && cinemachineCamera != null)
        {
            noiseComponent = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        if (noiseComponent == null)
        {
            Debug.LogError("Noise Component ");
        }
    }

    public void ShakeCamera() 
    {
        if (noiseComponent != null)
        {
            noiseComponent.AmplitudeGain = intensity;
            noiseComponent.FrequencyGain = frequency;
        }

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
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
                    noiseComponent.FrequencyGain = 0f;
                }
            }
        }
    }
}


















// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Unity.Cinemachine;

// public class CameraShake : MonoBehaviour
// {
//      [SerializeField] private CinemachineCamera cinemachineCamera;
//      [SerializeField] private CinemachineBasicMultiChannelPerlin noiseComponent;
//      [SerializeField] private float shakeTimer;
//      [SerializeField] private float shakeTimerTotal;
//      [SerializeField] private float startingIntensity;

//     [SerializeField] private float intensity;
//     [SerializeField] private float time;


//     private void Awake() 
//     {
//         if (cinemachineCamera == null)
//             cinemachineCamera = GetComponent<CinemachineCamera>();

//         if (noiseComponent == null && cinemachineCamera != null)
//             noiseComponent = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
//     }


//     // private void Awake() 
//     // {
//     //     cinemachineCamera = GetComponentInParent<CinemachineCamera>();
//     //     if (cinemachineCamera == null)
//     //     {
//     //         cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
//     //     }
        
//     //     if (cinemachineCamera != null)
//     //     {
//     //         noiseComponent = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
//     //     }
//     // }

//     public void ShakeCamera() 
//     {
//         if (noiseComponent != null)
//         {
//             noiseComponent.AmplitudeGain = intensity;
//         }

//         startingIntensity = intensity;
//         shakeTimerTotal = time;
//         shakeTimer = time;

//         Debug.Log("Camera is shaking");
//     }

//     private void Update() 
//     {
//         if (shakeTimer > 0) 
//         {
//             shakeTimer -= Time.deltaTime;
            
//             if (noiseComponent != null)
//             {
//                 if (shakeTimer > 0f)
//                 {
//                     noiseComponent.AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
//                 }
//                 else
//                 {
//                     noiseComponent.AmplitudeGain = 0f;
//                 }
//             }
//         }
//     }
// }


    
  