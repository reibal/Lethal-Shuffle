using UnityEngine;

public class CardPreview : MonoBehaviour
{
    public static CardPreview Instance;

    private GameObject previewInstance;
    private bool isEnabled = true;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("The game tried to create a second instance of CardPreview");
        }
        Instance = this;
    }

    private void Start()
    {
        HidePreview();
    }

    public void ShowPreview(GameObject cardGameObject)
    {
        HidePreview();
        if (!isEnabled || cardGameObject == null) return;

        previewInstance = Instantiate(cardGameObject, transform);
        previewInstance.name = "CardPreviewClone";

        // disable interactions on the preview clone
        if (previewInstance.TryGetComponent<CanvasGroup>(out var canvasGroup))
        {
            canvasGroup.blocksRaycasts = false;
        }

        if (previewInstance.TryGetComponent<CardDisplay>(out var cardDisplay))
        {
            cardDisplay.isInteractable = false;
        }

        // Reset transform so the preview is aligned correctly under the preview parent
        RectTransform previewRect = previewInstance.GetComponent<RectTransform>();
        if (previewRect != null)
        {
            previewRect.anchorMin = Vector2.one;
            previewRect.anchorMax = Vector2.one;
            previewRect.pivot = new Vector2(1f, 1f);
            previewRect.anchoredPosition = Vector2.zero;
            previewRect.localScale = Vector3.one * 1.6f;
        }

        // Also align the CardVisual child to top-right
        RectTransform cardVisualRect = previewInstance.transform.Find("CardVisual") as RectTransform;
        if (cardVisualRect != null)
        {
            cardVisualRect.anchorMin = Vector2.one;
            cardVisualRect.anchorMax = Vector2.one;
            cardVisualRect.pivot = Vector2.one;
            cardVisualRect.anchoredPosition = Vector2.zero;
        }
    }

    public void HidePreview()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
    }

    public void EnablePreview()
    {
        isEnabled = true;
    }

    public void DisablePreview()
    {
        isEnabled = false;
    }
}
