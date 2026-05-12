using UnityEngine;

public interface ICard
{
    string CardName { get; }
    string Description { get; }
    Sprite Artwork { get; }
}
