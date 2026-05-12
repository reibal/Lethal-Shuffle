using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EntitySlotDropzone : MonoBehaviour, IDropHandler
{
    private Image entityImage;
    private Entity currentEntity;

    [SerializeField] private Sprite defaultDropzoneSprite;
    private Color defaultDropzoneTint;

    void Awake()
    {
        entityImage = GetComponent<Image>();
        defaultDropzoneTint = entityImage.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (currentEntity != null) return;
        if (!eventData.pointerDrag.TryGetComponent<CardDisplay>(out var draggedCard)) return;
        if (draggedCard.CardData is not EntityCard entityCard) return;

        currentEntity = new Entity(entityCard);
        entityImage.sprite = entityCard.characterSprite;
        entityImage.color = new Color(1, 1, 1);

        draggedCard.PlayCard();
        Destroy(draggedCard.gameObject);
    }

    public void ClearSlot()
    {
        currentEntity = null;
        entityImage.sprite = defaultDropzoneSprite;
        entityImage.color = defaultDropzoneTint;
    }

    public bool IsOccupied => currentEntity != null;
}