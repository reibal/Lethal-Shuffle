using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Heal")]
public class HealingEffect : CardEffect
{
    public int healAmount;

    public override void Execute(Entity user, Entity target)
    {
        if (target != null)
        {
            target.Heal(healAmount);
            Debug.Log($"{user.EntityName} healed {healAmount} health points to {target.EntityName}");
        }
    }
}