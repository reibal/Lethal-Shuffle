using UnityEngine;

[CreateAssetMenu(fileName = "EntityCard", menuName = "Cards/Create Entity Card")]
public class EntityCard : CardData, IPlayableCard, IBattleCard
{
    [Header("Requirements")]
    [SerializeField] private int manaCost;

    [Header("Stats")]
    [SerializeField] private int attackDamage;
    [SerializeField] private int hp;

    [Header("Art")]
    public Sprite characterSprite;

    public int ManaCost { get => manaCost; }
    public int AttackDamage { get => attackDamage; }
    public int HP { get => hp; }
    public Sprite CharacterSprite { get => characterSprite; }
}
