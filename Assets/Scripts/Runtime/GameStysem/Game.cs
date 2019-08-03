using System;
using UnityEngine;

public class Game
{
    public void Attack(Actor me, Actor target, int strength)
    {
        Debug.Log($"<b>Game:</b> <b>{me.Name}</b> attacked <b>{target.Name}</b> dealing {strength} damage");
        target.Damage(strength);
    }
}