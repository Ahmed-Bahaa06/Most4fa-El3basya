using UnityEngine;
using UnityEngine.UI;

public class LampFlickerAlpha : MonoBehaviour
{
    private Image uiImage;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float minAlpha = 0.2f; 
    [SerializeField] private float maxAlpha = 1.0f; 
    
    [SerializeField] private float minTime = 0.05f;
    [SerializeField] private float maxTime = 0.2f;
    private float timer;

    void Start()
    {
        uiImage = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float randomAlpha = Random.Range(minAlpha, maxAlpha);

            if (uiImage != null)
            {
                Color c = uiImage.color;
                c.a = randomAlpha;
                uiImage.color = c;
            }
            else if (spriteRenderer != null)
            {
                Color c = spriteRenderer.color;
                c.a = randomAlpha;
                spriteRenderer.color = c;
            }

            timer = Random.Range(minTime, maxTime);
        }
    }
}
