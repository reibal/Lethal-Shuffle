using UnityEngine;

// A class to represent data for any card that can be played DURING the game (excluding Champions/Bosses)
public interface IPlayableCard : ICard
{
    int ManaCost { get; }

    // TODO: Reconsider if this should be necessary for every PlayableCard
    //public List<EffectEntry> Effects { get => effects; }
}