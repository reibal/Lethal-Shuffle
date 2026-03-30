using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems; // Required for Pointer Event Interfaces (without those, pointer events don't work)

[RequireComponent(typeof(LayoutElement))]
[RequireComponent(typeof(CanvasGroup))]
public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region [EDITOR REFERENCES] UI Component References
    [Header("UI Component References")]
    [SerializeField] private Transform cardVisual;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI tagText;
    [SerializeField] private Image artworkImage;
    #endregion

    #region [VARS] Basic variables
    private Card cardData;
    private float cardPreferredWidth;
    #endregion

    #region [VARS] Hover and Drag & Drop variables
    private Coroutine hoverCoroutine;
    private GameObject draggedCardPlaceholder = null;
    private int draggedCardNewSiblingIndex = 0;
    private static bool IsDraggingAnyCard = false;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    #endregion

    // Load necessary components and their relevant info
    void Awake()
    {
        // Having this CanvasGroup lets us "ignore" the card later, so the mouse can see what's BEHIND it (the board)
        canvasGroup = GetComponent<CanvasGroup>();
        cardPreferredWidth = GetComponent<LayoutElement>().preferredWidth;
    }

    // HandManager needs to call Initialize() to "fill in" the card's info
    public void Initialize(Card data)
    {
        cardData = data;
        nameText.text = data.cardName;
        descriptionText.text = data.description;
        manaText.text = data.manaCost.ToString();
        tagText.text = data.requiredTag.ToString();

        if (data.artwork != null)
        {
            artworkImage.sprite = data.artwork;
        }
    }

    #region [POINTER EVENTS]: Hover

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsDraggingAnyCard) return;
        StopHoverAnimation();
        // Lift by 50 units and grow to 1.2x size
        hoverCoroutine = StartCoroutine(AnimateHover(new Vector3(0, 60, 0), new Vector3(1.1f, 1.1f, 1)));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsDraggingAnyCard) return;
        StopHoverAnimation();
        // Return to center and normal size
        hoverCoroutine = StartCoroutine(AnimateHover(Vector3.zero, Vector3.one));
    }

    #endregion

    #region [POINTER EVENTS]: Drag & Drop

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsDraggingAnyCard) return;
        IsDraggingAnyCard = true;
        StopHoverAnimation();
        cardVisual.localPosition = Vector3.zero;
        cardVisual.localScale = Vector3.one;

        // Make the card slightly smaller for dragging
        StartCoroutine(AnimateHover(Vector3.zero, new Vector3(0.8f, 0.8f, 1)));

        // Create the placeholder for the reserved slot
        draggedCardPlaceholder = new GameObject("CardPlaceholder");
        draggedCardPlaceholder.transform.SetParent(transform.parent);
        draggedCardPlaceholder.transform.localScale = Vector3.one;
        // Create a LayoutElement component to define the width of the placeholder
        LayoutElement placeholderLayoutElement = draggedCardPlaceholder.AddComponent<LayoutElement>();
        placeholderLayoutElement.preferredWidth = cardPreferredWidth;

        // Put the placeholder exactly where the card was before starting the dragging
        draggedCardPlaceholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        // Finally extract the card from its parent so it's free to move
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        // And disable blocking raycasts so we can detect what's under the mouse, through the card
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Follow the mouse perfectly
        transform.position = eventData.position;

        for (int i = 0; i < originalParent.childCount; i++)
        {
            if (transform.position.x < (originalParent.GetChild(i).position.x + (cardPreferredWidth * 0.3)))
            {
                draggedCardNewSiblingIndex = i;
                if (draggedCardPlaceholder.transform.GetSiblingIndex() != draggedCardNewSiblingIndex)
                {
                    draggedCardPlaceholder.transform.SetSiblingIndex(draggedCardNewSiblingIndex);
                }
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);

        // Snap to the placeholder's spot
        transform.SetSiblingIndex(draggedCardPlaceholder.transform.GetSiblingIndex());

        // Clean up
        Destroy(draggedCardPlaceholder);

        canvasGroup.blocksRaycasts = true;
        cardVisual.localPosition = Vector3.zero;
        cardVisual.localScale = Vector3.one;

        IsDraggingAnyCard = false;
        hoverCoroutine = StartCoroutine(AnimateHover(Vector3.zero, Vector3.one));
    }

    #endregion

    #region [ANIMATION]: Draw Card Animation

    public IEnumerator StartCardDrawAnimation()
    {
        LayoutElement layout = GetComponent<LayoutElement>();
        float duration = 0.5f;
        float elapsed = 0f;
        // Start way off to the right (Screen.width as localPosition to a point in the center means half screen size outside from the right side)
        Vector3 startPosition = new Vector3(Screen.width, cardVisual.localPosition.y, 0);
        cardVisual.localPosition = startPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = elapsed / duration;
            // Use an easing curve for smoother movement (Optional but looks better)
            float curve = Mathf.SmoothStep(0, 1, percent);

            // Move the visual child
            cardVisual.localPosition = Vector3.Lerp(startPosition, Vector3.zero, curve);

            float percentSecondHalf = Mathf.Min(Mathf.Max(percent - 0.65f, 0) * 3f, 1f);
            float currentWidth = Mathf.SmoothStep(0, cardPreferredWidth, percentSecondHalf);
            layout.preferredWidth = currentWidth;

            yield return null; // Wait for the very next frame
        }

        cardVisual.localPosition = Vector3.zero; // Snap to perfect center at end
    }

    #endregion

    #region [ANIMATION]: Hover Animation

    private IEnumerator AnimateHover(Vector3 targetPos, Vector3 targetScale)
    {
        float duration = 0.15f;
        float elapsed = 0f;
        Vector3 startPos = cardVisual.localPosition;
        Vector3 startScale = cardVisual.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = elapsed / duration;

            cardVisual.localPosition = Vector3.Lerp(startPos, targetPos, percent);
            cardVisual.localScale = Vector3.Lerp(startScale, targetScale, percent);
            yield return null;
        }

        cardVisual.localPosition = targetPos;
        cardVisual.localScale = targetScale;
    }

    private void StopHoverAnimation()
    {
        if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);
    }

    #endregion

    #region [(WIP) - GAMEPLAY]: Play a card
    // (WIP): Play a card on the board
    public void PlayCard()
    {
        Debug.Log($"Playing {cardData.name}");
    }
    #endregion

}