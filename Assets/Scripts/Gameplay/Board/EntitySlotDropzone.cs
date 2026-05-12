using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class EntitySlotDropzone : MonoBehaviour, IDropHandler
{
    private Image slotBackgroundImage;
    private EntityOnBoard currentEntity;

    [SerializeField] private Sprite defaultDropzoneSprite;
    private Color defaultDropzoneTint;

    [SerializeField] private EntityOnBoard entityOnBoardPrefab;

    void Awake()
    {
        slotBackgroundImage = GetComponent<Image>();
        defaultDropzoneTint = slotBackgroundImage.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (currentEntity != null) return;
        if (!eventData.pointerDrag.TryGetComponent<CardDisplay>(out var draggedCard)) return;
        if (draggedCard.CardData is not EntityCard entityCard) return;

        //entityImage.sprite = entityCard.characterSprite;
        slotBackgroundImage.color = new Color(1, 1, 1);
        SetEntity(new Entity(entityCard));

        draggedCard.PlayCard();
        Destroy(draggedCard.gameObject);
    }

    public void SetEntity(Entity entity)
    {
        EntityOnBoard entityOnBoard = Instantiate(entityOnBoardPrefab, transform);
        entityOnBoard.Initialize(entity.AttackPower, entity.CurrentHP, entity.EntitySprite);
        currentEntity = entityOnBoard;
    }

    public void ClearSlot()
    {
        currentEntity = null;
        slotBackgroundImage.color = defaultDropzoneTint;
    }

    public bool IsOccupied => currentEntity != null;
}