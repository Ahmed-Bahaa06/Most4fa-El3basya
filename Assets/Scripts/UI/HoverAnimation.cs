using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private bool rotate = true;
    [SerializeField] private float rotateValue = 15f;
    [SerializeField] private bool scale = true;
    [SerializeField] private float scaleValue = 1.1f;
    [SerializeField] private float animationTime = 1f;
    private Vector3 originalScale = Vector3.one;
    private Vector3 originalRotationEuler = Vector3.zero;
    private Vector3 rotationAmount;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        rotationAmount = new Vector3(0, 0, rotateValue);
        originalScale = transform.localScale;
        originalRotationEuler = transform.localEulerAngles;
    }

    private void OnEnable() {
        transform.DOKill();
        transform.localScale = originalScale;
        transform.localEulerAngles = originalRotationEuler;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Application.isMobilePlatform) return;
        if (button != null && !button.interactable) return;

        transform.DOKill();

        if (rotate) {
            transform.DORotate(rotationAmount, animationTime).SetEase(Ease.OutBack).SetUpdate(true);
        }
        if (scale) {
            transform.DOScale(originalScale * scaleValue, animationTime).SetEase(Ease.InOutQuad).SetUpdate(true);
        }
    }

    private void OnDisable() {
        transform.DOKill();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (Application.isMobilePlatform) return;
        if (button != null && !button.interactable) return;

        transform.DOKill();

        if (rotate) {
            transform.DORotate(originalRotationEuler, animationTime).SetEase(Ease.OutBack).SetUpdate(true);
        }
        if (scale) {
            transform.DOScale(originalScale, animationTime).SetEase(Ease.InOutQuad).SetUpdate(true);
        }
    }
}
