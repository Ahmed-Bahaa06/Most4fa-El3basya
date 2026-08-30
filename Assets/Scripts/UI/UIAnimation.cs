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


    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.root.GetComponent<RectTransform>();
    }

    private void OnEnable() {
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
        Vector3 starting = up ? Vector3.zero : Vector3.one;
        Vector3 ending = up ? Vector3.one : Vector3.zero;
        transform.DOScale(ending, animationTime)
            .From(starting).SetEase(Ease.OutExpo).SetDelay(delay).SetUpdate(true);
    }
    private void Slide(Vector2 direction) {
        Vector2 originalPosition = rectTransform.localPosition;
        Vector2 StartingPosition = Vector2.zero;
        switch (direction.y) {
            case 1: StartingPosition.y = originalPosition.y - canvas.rect.height; break;
            case -1: StartingPosition.y = originalPosition.y + canvas.rect.height; break;
        }
        switch (direction.x) {
            case 1: StartingPosition.x = originalPosition.x - canvas.rect.width; break;
            case -1: StartingPosition.x = originalPosition.x + canvas.rect.width; break;
        }

        rectTransform.DOLocalMove(originalPosition, animationTime).From(StartingPosition)
            .SetEase(Ease.OutCubic).SetDelay(delay).SetUpdate(true);
    }
    private void OnDisable() {
        transform.DOKill();
        rectTransform.DOKill();
    }
}
