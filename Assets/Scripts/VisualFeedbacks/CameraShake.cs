using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;


public class CameraShake : MonoBehaviour
{

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    [SerializeField] private float intensity;
    [SerializeField] private float time;

    private void Awake() {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera() {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;

        Debug.Log("Camera is shaking");
    }

    private void Update() {
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}
