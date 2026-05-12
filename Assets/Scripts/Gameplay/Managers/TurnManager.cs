using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public enum TurnOwner
    {
        Player,
        Enemy
    }

    public enum TurnPhase
    {
        None,
        Draw,
        Play,
        Attack,
        TurnChange
    }

    [Header("References")]
    [SerializeField] private HandManager handManager;
    [SerializeField] private UIManager uiManager;

    [Header("Economy")]
    [SerializeField] private int startingHandSize = 5;
    [SerializeField] private int cardsToDrawPerTurn = 2;

    [Header("UI")]

    [Header("Events")]
    public UnityEvent OnPlayerTurnStarted;
    public UnityEvent OnEnemyTurnStarted;
    public UnityEvent OnPhaseChanged;
    public UnityEvent OnTurnChanged;
    public UnityEvent OnPlayerTurnEnded;
    public UnityEvent OnEnemyTurnEnded;

    private TurnOwner activeOwner = TurnOwner.Player;
    private TurnPhase activePhase = TurnPhase.None;
    private int turnNumber;

    public bool IsPlayerTurn => activeOwner == TurnOwner.Player;
    public TurnPhase CurrentPhase => activePhase;

    private void Start()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        if (handManager == null)
        {
            Debug.LogError("TurnManager requires a HandManager reference.");
            yield break;
        }

        if (uiManager != null)
        {
            // Pretend that we are in "Draw" phase (UI only) while drawing initial cards
            uiManager.SetPhase(TurnPhase.Draw, activeOwner);
        }

        turnNumber = 0;
        activeOwner = TurnOwner.Player;
        activePhase = TurnPhase.None;

        handManager.InitializeDeck();
        yield return handManager.DrawCardsRoutine(startingHandSize);

        BeginTurn(activeOwner);
    }

    private void BeginTurn(TurnOwner owner)
    {
        activeOwner = owner;
        turnNumber++;
        OnTurnChanged?.Invoke();

        if (activeOwner == TurnOwner.Player)
        {
            OnPlayerTurnStarted?.Invoke();

            if (turnNumber == 1)
            {
                ChangePhase(TurnPhase.Play);
            }
            else
            {
                ChangePhase(TurnPhase.Draw);
            }
        }
        else
        {
            OnEnemyTurnStarted?.Invoke();
            ChangePhase(TurnPhase.Play);
        }
    }

    private void ChangePhase(TurnPhase nextPhase)
    {
        activePhase = nextPhase;

        if (uiManager != null)
        {
            uiManager.SetPhase(nextPhase, activeOwner);
        }

        OnPhaseChanged?.Invoke();

        switch (nextPhase)
        {
            case TurnPhase.Draw:
                HandleDrawPhase();
                break;
            case TurnPhase.Play:
                HandlePlayPhase();
                break;
            case TurnPhase.Attack:
                HandleAttackPhase();
                break;
            case TurnPhase.TurnChange:
                HandleTurnChangePhase();
                break;
        }
    }

    private void HandleDrawPhase()
    {
        if (activeOwner == TurnOwner.Player)
        {
            StartCoroutine(PerformDrawThenPlay());
        }
        else
        {
            ChangePhase(TurnPhase.Play);
        }
    }

    private IEnumerator PerformDrawThenPlay()
    {
        handManager.DrawCards(cardsToDrawPerTurn);
        while (handManager.IsDrawing)
        {
            yield return null;
        }

        ChangePhase(TurnPhase.Play);
    }

    private void HandlePlayPhase()
    {
        if (uiManager != null)
        {
            uiManager.SetEndTurnVisible(activeOwner == TurnOwner.Player);
        }

        if (activeOwner == TurnOwner.Enemy)
        {
            StartCoroutine(EnemyPlayRoutine());
        }
    }

    private IEnumerator EnemyPlayRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("[NIY] Enemy play phase placeholder: no AI implemented yet.");
        ChangePhase(TurnPhase.Attack);
    }

    private void HandleAttackPhase()
    {
        Debug.Log("[NIY] Resolving attack phase placeholder: not implemented yet.");
        ChangePhase(TurnPhase.TurnChange);
    }

    private void HandleTurnChangePhase()
    {
        StartCoroutine(TurnChangeRoutine());
    }

    private IEnumerator TurnChangeRoutine()
    {
        yield return new WaitForSeconds(1f);
        SwitchOwner();
    }

    private void SwitchOwner()
    {
        activeOwner = (activeOwner == TurnOwner.Player)
            ? TurnOwner.Enemy
            : TurnOwner.Player;
        BeginTurn(activeOwner);
    }

    public void EndTurnButton()
    {
        if (activeOwner != TurnOwner.Player)
        {
            Debug.LogWarning("Only the player can end the player turn.");
            return;
        }

        if (activePhase != TurnPhase.Play)
        {
            Debug.LogWarning($"Cannot end turn during phase {activePhase}. Wait until the Play phase.");
            return;
        }

        if (uiManager != null)
        {
            uiManager.SetEndTurnVisible(false);
        }

        ChangePhase(TurnPhase.Attack);
    }
}
