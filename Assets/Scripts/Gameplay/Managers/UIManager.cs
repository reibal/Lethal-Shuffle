using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Phase Icons")]
    [SerializeField] private Image drawPhaseIcon;
    [SerializeField] private Image playPhaseIcon;
    [SerializeField] private Image attackPhaseIcon;
    [SerializeField] private Image turnChangeIcon;

    [Header("End Turn Button")]
    [SerializeField] private Button endTurnButton;

    [Header("Icon Colors")]
    [SerializeField] private Color playerActiveColor = new Color(0.35f, 1f, 0.35f, 1f);
    [SerializeField] private Color enemyActiveColor = new Color(1f, 0.35f, 0.35f, 1f);
    [SerializeField] private Color inactiveColor = Color.white;

    public void SetPhase(TurnManager.TurnPhase phase, TurnManager.TurnOwner turnOwner)
    {
        ResetPhaseIcons();
        Color activeColor = (turnOwner == TurnManager.TurnOwner.Player)
            ? playerActiveColor
            : enemyActiveColor;

        switch (phase)
        {
            case TurnManager.TurnPhase.Draw:
                SetIconColor(drawPhaseIcon, activeColor);
                break;
            case TurnManager.TurnPhase.Play:
                SetIconColor(playPhaseIcon, activeColor);
                break;
            case TurnManager.TurnPhase.Attack:
                SetIconColor(attackPhaseIcon, activeColor);
                break;
            case TurnManager.TurnPhase.TurnChange:
                SetIconColor(turnChangeIcon, activeColor);
                break;
            case TurnManager.TurnPhase.None:
            default:
                break;
        }
    }

    public void SetEndTurnVisible(bool visible)
    {
        if (endTurnButton == null) return;
        endTurnButton.gameObject.SetActive(visible);
    }

    public void ResetPhaseIcons()
    {
        SetIconColor(drawPhaseIcon, inactiveColor);
        SetIconColor(playPhaseIcon, inactiveColor);
        SetIconColor(attackPhaseIcon, inactiveColor);
        SetIconColor(turnChangeIcon, inactiveColor);
    }

    private void SetIconColor(Image image, Color color)
    {
        if (image == null) return;
        image.color = color;
    }
}
