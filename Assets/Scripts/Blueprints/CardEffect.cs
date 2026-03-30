using UnityEngine;

// This is the "Base" for every effect (Damage, Heal, Mana, etc.)
public abstract class CardEffect : ScriptableObject 
{
    public abstract void Execute(Entity user, Entity target);
}