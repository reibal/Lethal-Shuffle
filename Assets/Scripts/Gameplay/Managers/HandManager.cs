using System.Collections;
using System.Collections.Generic;
using System.Linq; // <-- Used to change types of a list
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private CardDisplay cardPrefab;
    [SerializeField] private EntityCardDisplay entityCardPrefab;
    [Header("GameObjects")]
    [SerializeField] private Transform handArea;

    // TODO: Remove this on production, where the player will set the deck instead
    [Header("Default Deck (DEBUG)")]
    public DeckTemplate defaultDeck;

    private Deck playerDeck;
    private readonly List<IPlayableCard> cardsInHand = new();
    private readonly int MAX_CARDS_IN_HAND = 10;

    private int pendingDrawRequests;
    private bool isDrawing;
    public bool IsDrawing => isDrawing || pendingDrawRequests > 0;

    private void Start()
    {
        InitializeDeck();
    }

    public void InitializeDeck()
    {
        // "Parse" all cards to `IPlayableCard`s
        List<IPlayableCard> deckCards = defaultDeck.startingCards
            .OfType<IPlayableCard>()
            .ToList();

        if (deckCards.Count != defaultDeck.startingCards.Count)
        {
            Debug.LogWarning("Deck contains non-playable CardData!");
        }

        playerDeck = new Deck(deckCards);
        playerDeck.Shuffle();
    }

    public void Draw()
    {
        DrawCards(1);
    }

    public void DrawCards(int count)
    {
        if (count <= 0) return;
        pendingDrawRequests += count;

        if (!isDrawing)
        {
            StartCoroutine(ProcessDrawQueue());
        }
    }

    public IEnumerator DrawCardsRoutine(int count)
    {
        DrawCards(count);
        while (IsDrawing)
        {
            yield return null;
        }
    }

    private IEnumerator ProcessDrawQueue()
    {
        while (pendingDrawRequests > 0)
        {
            if (isDrawing)
            {
                yield return null;
                continue;
            }

            if (cardsInHand.Count >= MAX_CARDS_IN_HAND)
            {
                Debug.LogWarning($"You already have {MAX_CARDS_IN_HAND} cards in hand, no more cards will be drawn");
                pendingDrawRequests = 0;
                yield break;
            }

            if (playerDeck == null)
            {
                Debug.LogWarning("Deck is not initialized yet. Call InitializeDeck() before drawing.");
                pendingDrawRequests = 0;
                yield break;
            }

            IPlayableCard drawnCard = playerDeck.Draw();
            if (drawnCard == null)
            {
                Debug.LogWarning("There are no more cards to draw from the deck");
                pendingDrawRequests = 0;
                yield break;
            }

            isDrawing = true;
            cardsInHand.Add(drawnCard);
            StartCoroutine(InstantiateCardInHand(drawnCard));

            while (isDrawing)
            {
                yield return null;
            }

            pendingDrawRequests--;
            yield return null;
        }
    }

    // Coroutine to draw a card with animation
    private IEnumerator InstantiateCardInHand(IPlayableCard drawnCard)
    {
        GameObject newCardInstance;
        if (drawnCard is EntityCard drawnEntityCard)
        {
            newCardInstance = Instantiate(entityCardPrefab.gameObject, handArea);
            EntityCardDisplay entityCardDisplay = newCardInstance.GetComponent<EntityCardDisplay>();
            entityCardDisplay.Initialize(drawnEntityCard);
            yield return entityCardDisplay.StartCardDrawAnimation();
        }
        else
        {
            newCardInstance = Instantiate(cardPrefab.gameObject, handArea);
            CardDisplay cardDisplay = newCardInstance.GetComponent<CardDisplay>();
            cardDisplay.Initialize(drawnCard);
            yield return cardDisplay.StartCardDrawAnimation();
        }

        isDrawing = false;
    }
}
