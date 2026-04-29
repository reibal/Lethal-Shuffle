using UnityEngine;

[CreateAssetMenu(fileName = "Create New Ability Card", menuName = "Cards")]
public class AbilityCard : Card
{
    [Header("Requirements")]
    public CardEnums.CardTag requiredTag; // e.g.: "None", "Martial", "Sneaky"...
}
