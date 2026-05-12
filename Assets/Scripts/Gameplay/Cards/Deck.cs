using UnityEngine;
using System.Collections.Generic;

public class Deck
{
    public List<IPlayableCard> cards;

    public Deck(List<IPlayableCard> cards)
    {
        this.cards = cards;
    }

    public IPlayableCard Draw()
    {
        if (cards.Count == 0)
        {
            return null;
        }

        IPlayableCard card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            IPlayableCard firstCard = cards[i];
            int j = Random.Range(i, cards.Count);
            IPlayableCard secondCard = cards[j];
            cards[i] = secondCard;
            cards[j] = firstCard;
        }
    }
}
