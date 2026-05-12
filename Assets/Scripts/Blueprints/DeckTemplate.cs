using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Deck Template", menuName = "Decks/Create Deck Template")]
public class DeckTemplate : ScriptableObject
{
    public string deckName;
    public List<CardData> startingCards;
}
