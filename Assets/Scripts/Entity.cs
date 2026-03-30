using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [Header("Information")]
    public string entityName;
    public List<CardEnums.CardTag> entityTags;

    [Header("Stats")] // Must be set for each entity
    public int maxHealth;
    public int basicAttackPower; // <-- The once-per-turn basic Attack power (can be buffed later)
    public int CurrentHealth { get; private set; } // <-- Will start fully healed, so no need to explicitly set it

    // This is great for updating your UI automatically!
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;

    private void Awake()
    {
        // Start fully healed
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(); // Tell the UI to update!

        if (CurrentHealth <= 0) Die();
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke();
    }

    private void Die()
    {
        Debug.Log(entityName + " has been defeated!");
        OnDeath?.Invoke();
    }
}