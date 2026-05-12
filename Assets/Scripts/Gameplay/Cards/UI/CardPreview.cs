using UnityEngine;

public class CardPreview : MonoBehaviour
{
    public static CardPreview Instance;
    private CardDisplay cardDisplay;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("The game tried to create a second instance of CardPreview");
        }
        Instance = this;
    }

    void Start()
    {
        cardDisplay = GetComponent<CardDisplay>();
        HidePreview();
    }

    public void ShowPreview(IPlayableCard cardData)
    {
        cardDisplay.Initialize(cardData);
        cardDisplay.ShowCard();
    }

    public void HidePreview()
    {
        cardDisplay.HideCard();
    }

}
