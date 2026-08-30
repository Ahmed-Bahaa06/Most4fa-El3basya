using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SpotlightFlicker : MonoBehaviour
{
    [Header("Flicker Intensity")]
     [SerializeField] private float minIntensity = 0.5f;
     [SerializeField] private float maxIntensity = 2.0f;

    [Header("Flicker Speed")]
  
     [SerializeField] private float minFlickerSpeed = 0.05f;
     [SerializeField] private float maxFlickerSpeed = 0.2f;

    private Light spotLight;

    void Start()
    {
        spotLight = GetComponent<Light>();

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            
            spotLight.intensity = Random.Range(minIntensity, maxIntensity);
            
           
            float randomDelay = Random.Range(minFlickerSpeed, maxFlickerSpeed);
            yield return new WaitForSeconds(randomDelay);
        }
    }
}