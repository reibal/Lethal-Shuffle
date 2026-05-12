using UnityEngine;

public interface IBattleCard: ICard
{
    int AttackDamage { get; }
    int HP { get; }
    Sprite EntitySprite { get; }
}