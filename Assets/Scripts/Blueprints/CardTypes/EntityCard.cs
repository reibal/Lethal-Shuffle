using UnityEngine;

[CreateAssetMenu(fileName = "Create New Entity Card", menuName = "Cards")]
public class EntityCard : Card
{
    [Header("Stats")]
    public int attackDamage;
    public int maxHp;
}
