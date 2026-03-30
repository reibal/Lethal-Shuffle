using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Damage")]
public class DamageEffect : CardEffect
{
    public int damageAmount;

    public override void Execute(Entity user, Entity target)
    {
        if (target != null)
        {
            target.TakeDamage(damageAmount);
            Debug.Log($"{user.entityName} dealt {damageAmount} damage to {target.entityName}");
        }
    }
}