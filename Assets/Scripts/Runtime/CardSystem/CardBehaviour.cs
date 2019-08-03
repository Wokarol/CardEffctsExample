using UnityEngine;

public abstract class CardBehaviour : ScriptableObject
{
    public abstract DataTypes DataMask { get; }
    public abstract void Execute(Game game, Actor me, Actor target, CardData data);
}