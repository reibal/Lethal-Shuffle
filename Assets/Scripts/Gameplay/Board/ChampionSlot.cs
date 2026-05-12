using UnityEngine;

public class ChampionSlot : MonoBehaviour
{

    [SerializeField] private EntityOnBoard entityOnBoardPrefab;

    public void SetChampion(ChampionCard championCard)
    {
        EntityOnBoard championEntityOnBoard = Instantiate(entityOnBoardPrefab, transform);
        championEntityOnBoard.Initialize(championCard.AttackDamage, championCard.HP, championCard.EntitySprite);
    }
}
