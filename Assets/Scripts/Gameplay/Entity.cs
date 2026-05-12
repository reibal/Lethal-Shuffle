using UnityEngine;
using UnityEngine.Events;

public class Entity
{
    public string EntityName { get; private set; }
    public int AttackPower { get; private set; }
    public int CurrentHP { get; private set; }
    public int MaxHP { get; private set; }
    public Sprite EntitySprite { get; private set; }

    // TODO: (Usage Pending) This is great for updating your UI automatically!
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;

    public Entity(EntityCard entityCard)
    {
        EntityName = entityCard.CardName;
        AttackPower = entityCard.AttackDamage;
        MaxHP = entityCard.HP;
        EntitySprite = entityCard.EntitySprite;
        FullHeal();
    }

    public void TakeDamage(int amount)
    {
        CurrentHP -= amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);

        // Tell the UI to update:
        OnHealthChanged?.Invoke();

        if (CurrentHP <= 0) Die();
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);

        OnHealthChanged?.Invoke();
    }

    public void FullHeal()
    {
        CurrentHP = MaxHP;
    }

    private void Die()
    {
        Debug.Log(EntityName + " has been defeated!");
        OnDeath?.Invoke();
    }
}