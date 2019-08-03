using System;
using UnityEngine;

public class Game
{
    public void Attack(Actor me, Actor target, int strenght)
    {
        Debug.Log($"<b>Game:</b> <b>{me.Name}</b> attacked <b>{target.Name}</b> dealing {strenght} damage");
        target.Damage(strenght);
    }
}