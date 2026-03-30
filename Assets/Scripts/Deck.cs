using UnityEngine;
using System.Collections.Generic;

public class Deck
{
    public List<Card> cards;

    public Deck(List<Card> cards)
    {
        this.cards = cards;
    }

    public Card Draw()
    {
        if (cards.Count == 0)
        {
            return null;
        }

        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card firstCard = cards[i];
            int j = Random.Range(i, cards.Count);
            Card secondCard = cards[j];
            cards[i] = secondCard;
            cards[j] = firstCard;
        }
    }
}
