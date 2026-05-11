using UnityEngine;

[CreateAssetMenu(fileName = "AbilityCard", menuName = "Cards/Create Ability Card")]
public class AbilityCard : Card
{
    [Header("Requirements")]
    public CardEnums.CardTag requiredTag; // e.g.: "None", "Martial", "Sneaky"...
}
