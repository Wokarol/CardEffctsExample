using UnityEngine;

public abstract class CardBehaviour : ScriptableObject
{
    // Mask used for editor
    public abstract DataTypes DataMask { get; }

    // Executes card
    public abstract void Execute(Game game, Actor me, Actor target, CardData data);
}