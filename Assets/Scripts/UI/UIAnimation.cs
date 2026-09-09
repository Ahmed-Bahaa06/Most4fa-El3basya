using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    private enum AnimationType {
        None,
        ScaleUp,
        ScaleDown,
        SlidingLeft,
        SlidingRight,
        SlidingDown,
        SlidingUp
    }

    [SerializeField] private AnimationType animationType;
    [SerializeField] private float animationTime = 1;
    [SerializeField] private float delay = 0;

    private RectTransform rectTransform;
    private RectTransform canvas;

    private Vector2 originalPosition;
    private Vector3 originalScale;
    private bool hasCachedOriginals = false;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        if (transform.root != null) {
            canvas = transform.root.GetComponent<RectTransform>();
        }
        CacheOriginals();
    }

    private void CacheOriginals() {
        if (!hasCachedOriginals && rectTransform != null) {
            originalPosition = rectTransform.localPosition;
            originalScale = rectTransform.localScale;
            hasCachedOriginals = true;
        }
    }

    private void OnEnable() {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        if (canvas == null && transform.root != null) canvas = transform.root.GetComponent<RectTransform>();
        
        CacheOriginals();
        ResetToOriginal();

        switch (animationType) {
            case AnimationType.ScaleUp: Scale(true); break;
            case AnimationType.ScaleDown: Scale(false); break;
            case AnimationType.SlidingUp: Slide(Vector2.up); break;
            case AnimationType.SlidingDown: Slide(Vector2.down); break;
            case AnimationType.SlidingRight: Slide(Vector2.right); break;
            case AnimationType.SlidingLeft: Slide(Vector2.left); break;
        }
    }

    private void Scale(bool up) {
        Vector3 starting = up ? Vector3.zero : originalScale;
        Vector3 ending = up ? originalScale : Vector3.zero;
        transform.DOScale(ending, animationTime)
            .From(starting).SetEase(Ease.OutExpo).SetDelay(delay).SetUpdate(true);
    }

    private void Slide(Vector2 direction) {
        if (canvas == null) return;

        Vector2 startingPosition = originalPosition;
        switch (direction.y) {
            case 1: startingPosition.y = originalPosition.y - canvas.rect.height; break;
            case -1: startingPosition.y = originalPosition.y + canvas.rect.height; break;
        }
        switch (direction.x) {
            case 1: startingPosition.x = originalPosition.x - canvas.rect.width; break;
            case -1: startingPosition.x = originalPosition.x + canvas.rect.width; break;
        }

        rectTransform.DOLocalMove(originalPosition, animationTime)
            .From(startingPosition)
            .SetEase(Ease.OutCubic)
            .SetDelay(delay)
            .SetUpdate(true);
    }

    private void ResetToOriginal() {
        if (!hasCachedOriginals || rectTransform == null) return;
        transform.DOKill();
        rectTransform.DOKill();
        rectTransform.localPosition = originalPosition;
        rectTransform.localScale = originalScale;
    }

    private void OnDisable() {
        ResetToOriginal();
    }
}
