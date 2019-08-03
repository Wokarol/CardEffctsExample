using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Heal for Mana")]
public class HealForManaEffect : CardBehaviour
{
    public override DataTypes DataMask => DataTypes.Strenght | DataTypes.Cost;

    public override void Execute(Game game, Actor me, Actor target, CardData data)
    {
        if (me.Mana > data.Cost) {
            me.Heal(data.Strenght);
            me.LowerMana(data.Cost);
        } else {
            Debug.Log("Not enough mana");
        }
    }
}
