using UnityEngine;

[CreateAssetMenu(fileName = "EntityCard", menuName = "Cards/Create Entity Card")]
public class EntityCard : Card
{
    [Header("Stats")]
    public int attackDamage;
    public int hp;

    [Header("Art")]
    public Sprite characterSprite;
    
}
