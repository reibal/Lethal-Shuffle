using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EffectEntry
{
    public CardEffect effect;
    public int value;
    public CardEnums.EffectTargetType targetType;
}

public abstract class CardData : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField] private string cardName;
    [TextArea(3, 10)][SerializeField] private string description;
    [SerializeField] private Sprite artwork;

    [Header("Mechanics")]
    // Effects to happen when the card is played:
    [SerializeField] private List<EffectEntry> effects;

    public string CardName { get => cardName; }
    public string Description { get => description; }
    public Sprite Artwork { get => artwork; }

    public List<EffectEntry> Effects { get => effects; }
}