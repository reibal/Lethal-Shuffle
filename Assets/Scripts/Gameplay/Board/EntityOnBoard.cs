using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityOnBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Image entityImage;

    public void Initialize(int attackDamage, int hp, Sprite entitySprite)
    {
        UpdateUI(attackDamage, hp, entitySprite);
    }

    private void UpdateUI(int attackDamage, int hp, Sprite entitySprite)
    {
        attackText.text = attackDamage.ToString();
        hpText.text = hp.ToString();
        entityImage.sprite = entitySprite;
    }
}
