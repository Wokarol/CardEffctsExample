using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Damage")]
public class DamageEffect : CardBehaviour
{
    public override DataTypes DataMask => DataTypes.Strenght;

    public override void Execute(Game game, Actor me, Actor target, CardData data)
    {
        game.Attack(me, target, data.Strenght);
    }
}
