using UnityEngine;

[CreateAssetMenu(fileName = "ChampionCard", menuName = "Cards/Create Champion Card")]
public class ChampionCard : CardData, IBattleCard
{
    [Header("Stats")]
    [SerializeField] private int attackDamage;
    [SerializeField] private int hp;

    [Header("Art (when played)")]
    [SerializeField] private Sprite characterSprite;

    public int AttackDamage { get => attackDamage; }
    public int HP { get => hp; }
    public Sprite EntitySprite { get => characterSprite; }
}