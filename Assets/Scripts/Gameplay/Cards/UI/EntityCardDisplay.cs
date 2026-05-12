using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(LayoutElement))]
[RequireComponent(typeof(CanvasGroup))]
public class EntityCardDisplay : CardDisplay
{
    #region [EDITOR REFERENCES] UI Component References
    [Header("UI Component References")]
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI hpText;
    #endregion

    public void Initialize(EntityCard data)
    {
        CardData = data;
        nameText.text = data.CardName;
        descriptionText.text = data.Description;
        manaText.text = data.ManaCost.ToString();
        attackText.text = data.AttackDamage.ToString();
        hpText.text = data.HP.ToString();

        if (data.Artwork != null)
        {
            artworkImage.sprite = data.Artwork;
        }
    }

}