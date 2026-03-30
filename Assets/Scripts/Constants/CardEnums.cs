
public class CardEnums
{
    public enum CardTag
    {
        None = 0, // <-- "None" should allow the card to be on any deck
        Martial = 1,
        Sneaky = 2,
        Leader = 3
    }

    public enum EffectTargetType
    {
        Targeted = 0,   // The entity you click on
        Self = 1,       // The entity playing the card
        AllEnemies = 2, // Every enemy on the board
        AllAllies = 3   // Every ally (including yourself)
    }
}
