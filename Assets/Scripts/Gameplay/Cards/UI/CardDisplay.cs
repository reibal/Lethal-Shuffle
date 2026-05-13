using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems; // Required for Pointer Event Interfaces (without those, pointer events don't work)
using System.Collections.Generic; // Required for lists

[RequireComponent(typeof(LayoutElement))]
[RequireComponent(typeof(CanvasGroup))]
public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region [EDITOR REFERENCES] UI Component References
    [Header("UI Component References")]
    [SerializeField] protected Transform cardVisual;
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI descriptionText;
    [SerializeField] protected TextMeshProUGUI manaText;
    [SerializeField] protected Image artworkImage;
    #endregion

    #region [VARS] Basic variables
    // Store a reference to the Card Data, to pass them on when played
    public IPlayableCard CardData { get; protected set; }
    // Store the preferredWith of the Card, to use it for the placeholder when Drag&Drop-ing
    private float cardPreferredWidth;
    #endregion

    #region [VARS] Hover and Drag & Drop variables
    [Header("Interactions (hover, dragging...)")]
    // Make false to prevent events like hover and dragging
    public bool isInteractable = true;
    // Store the reference to the current hover coroutine, so we can stop it prematurely
    private Coroutine hoverCoroutine;
    // Store the reference to the draggedCardPlaceholder, so we can destroy the placeholder when dropping the card
    private GameObject draggedCardPlaceholder = null;
    // We will update this number when dragging, to know where to place it when dropping it
    private int draggedCardNewSiblingIndex = 0;
    // Store a reference to the original parent of the card, to bring the card back there when dropping
    private Transform originalParent;
    // The CanvasGroup of the
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
    public void Initialize(IPlayableCard data)
    {
        // TODO: Review initialization to make sure all three card types have their own UI
        CardData = data;
        nameText.text = data.CardName;
        descriptionText.text = data.Description;
        manaText.text = data.ManaCost.ToString();

        if (data.Artwork != null)
        {
            artworkImage.sprite = data.Artwork;
        }
    }

    #region [POINTER EVENTS]: Hover

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;
        StopHoverAnimation();
        // Lift by 50 units and grow to 1.2x size
        hoverCoroutine = StartCoroutine(AnimateHover(new Vector3(0, 60, 0), new Vector3(1.1f, 1.1f, 1)));
        CardPreview.Instance.ShowPreview(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;
        CardPreview.Instance.HidePreview();
        StopHoverAnimation();
        // Return to center and normal size
        hoverCoroutine = StartCoroutine(AnimateHover(Vector3.zero, Vector3.one));
    }

    #endregion

    #region [POINTER EVENTS]: Drag & Drop

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;
        StopHoverAnimation();
        cardVisual.localPosition = Vector3.zero;
        cardVisual.localScale = Vector3.one;

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
        CardPreview.Instance.DisablePreview();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;

        // Follow the mouse perfectly
        transform.position = eventData.position;

        // Loop through the cards in the hand, to check the new position for the card (and move the placeholder there)
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
        if (!isInteractable) return;

        // Detect what's under the cursor when dropping
        List<RaycastResult> raycastHits = new();
        EventSystem.current.RaycastAll(eventData, raycastHits);

        // Ability cards can be dropped anywhere on the Board
        if (CardData is AbilityCard && CheckDropOnBoard(raycastHits))
        {
            PlayCard();
            return;
        }

        // Return card to original position if not played
        transform.SetParent(originalParent);
        transform.SetSiblingIndex(draggedCardPlaceholder.transform.GetSiblingIndex());
        canvasGroup.blocksRaycasts = true;
        cardVisual.localPosition = Vector3.zero;
        cardVisual.localScale = Vector3.one;
        hoverCoroutine = StartCoroutine(AnimateHover(Vector3.zero, Vector3.one));

        Destroy(draggedCardPlaceholder);
        CardPreview.Instance.EnablePreview();
    }

    private bool CheckDropOnBoard(List<RaycastResult> raycastHits)
    {
        // Try to find a Board under the cursor
        BoardDropzone boardTarget = null;
        foreach (RaycastResult result in raycastHits)
        {
            boardTarget = result.gameObject.GetComponent<BoardDropzone>();
            if (boardTarget != null) break;
        }
        return boardTarget != null;
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

    #region [(WIP) - GAMEPLAY] Play a card
    // (WIP): Play a card on the board
    public void PlayCard()
    {
        Debug.Log($"Playing {CardData.CardName}");
        Destroy(gameObject); // Destroy the card from the hand
        if (draggedCardPlaceholder)
        {
            Destroy(draggedCardPlaceholder); // Destroy the placeholder from the hand
        }
        CardPreview.Instance.EnablePreview();
    }
    #endregion

    #region [(WIP) - Misc] Other methods (Show Card)
    public void ShowCard()
    {
        cardVisual.gameObject.SetActive(true);
    }
    public void HideCard()
    {
        cardVisual.gameObject.SetActive(false);
    }
    #endregion
}