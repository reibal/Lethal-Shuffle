using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EffectEntry
{
    public CardEffect effect;
    public int value;
    public CardEnums.EffectTargetType targetType;
}

public abstract class Card : ScriptableObject
{
    [Header("Base Settings")]
    public string cardName;
    [TextArea(3, 10)] public string description;
    public Sprite artwork;

    [Header("Mechanics")]
    public int manaCost;
    public List<EffectEntry> effects;
}