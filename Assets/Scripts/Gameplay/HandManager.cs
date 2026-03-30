using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject cardPrefab;
    [Header("GameObjects")]
    [SerializeField] private Transform handArea;

    // TODO: Remove this on production, this is for debugging purposes only
    [Header("Default Deck (DEBUG)")]
    public DeckTemplate defaultDeck;

    private Deck playerDeck;
    private readonly List<Card> cardsInHand = new();
    private readonly int MAX_CARDS_IN_HAND = 10;

    private enum HandManagerState
    {
        IDLE,
        DRAWING,
        PLAYING
    }

    private HandManagerState state = HandManagerState.IDLE;

    void Start()
    {
        // Create a new List, so we don't affect the one in the ScriptableObject:
        List<Card> deckCards = new(defaultDeck.startingCards);
        playerDeck = new Deck(deckCards);
        playerDeck.Shuffle();
    }

    public void Draw()
    {
        if (state != HandManagerState.IDLE)
        {
            Debug.LogWarning($"Cannot draw cards while on this state: {state}");
            return;
        }
        if (cardsInHand.Count >= MAX_CARDS_IN_HAND)
        {
            Debug.LogWarning($"You already have {MAX_CARDS_IN_HAND} cards in hand, no more cards will be drawn");
            return;
        }
        Card drawnCard = playerDeck.Draw();
        if (drawnCard == null)
        {
            Debug.LogWarning("There are no more cards to draw from the deck");
            return;
        }
        // Happy Path!
        state = HandManagerState.DRAWING;
        cardsInHand.Add(drawnCard);
        StartCoroutine(InstantiateCardInHand(drawnCard));
    }

    private IEnumerator InstantiateCardInHand(Card drawnCard)
    {
        GameObject newCardInstance = Instantiate(cardPrefab, handArea);
        CardDisplay cardDisplay = newCardInstance.GetComponent<CardDisplay>();
        cardDisplay.Initialize(drawnCard);
        yield return cardDisplay.StartCardDrawAnimation();
        state = HandManagerState.IDLE;
    }
}
