using UnityEngine;
using UnityEngine.Events;

public class Entity
{
    [Header("Information")]
    public string entityName;

    [Header("Stats")]
    public int basicAttackPower; // <-- The once-per-turn basic Attack power (can be buffed later)
    public int maxHealth;

    public int CurrentHealth { get; private set; }

    // (TODO: Usage Pending) This is great for updating your UI automatically!
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;

    public Entity(string entityName, int basicAttackPower, int maxHealth)
    {
        this.entityName = entityName;
        this.basicAttackPower = basicAttackPower;
        this.maxHealth = maxHealth;
        FullHeal();
    }

    public Entity(EntityCard entityCard)
    {
        entityName = entityCard.CardName;
        basicAttackPower = entityCard.AttackDamage;
        maxHealth = entityCard.HP;
        FullHeal();
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        // Tell the UI to update:
        OnHealthChanged?.Invoke();

        if (CurrentHealth <= 0) Die();
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke();
    }

    public void FullHeal()
    {
        CurrentHealth = maxHealth;
    }

    private void Die()
    {
        Debug.Log(entityName + " has been defeated!");
        OnDeath?.Invoke();
    }
}