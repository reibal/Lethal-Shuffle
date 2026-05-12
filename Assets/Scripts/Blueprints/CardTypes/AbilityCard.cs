using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityCard", menuName = "Cards/Create Ability Card")]
public class AbilityCard : CardData, IPlayableCard
{
    [Header("Requirements")]
    [SerializeField] private int manaCost;

    // TODO: Decide how to implement required tags (for now: ignore)
    public CardEnums.CardTag requiredTag; // e.g.: "None", "Martial", "Sneaky"...

    public int ManaCost { get => manaCost; }
}
