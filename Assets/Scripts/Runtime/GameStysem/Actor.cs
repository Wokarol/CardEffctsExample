using System;
using UnityEngine;

public class Actor
{
    public readonly string Name;

    public int Mana { get; private set; }
    public int Health { get; private set; }

    public Actor(string name, int mana, int health)
    {
        Name = name;
        Mana = mana;
        Health = health;
    }

    public void Heal(int value)
    {
        Debug.Log($" <b>Actor:</b> <b>{Name}</b> healed for {value} points");
        Health += value;
    }

    public void Damage(int value)
    {
        Debug.Log($" <b>Actor:</b> <b>{Name}</b> damaged for {value} points");
        Health -= value;
    }

    public void LowerMana(int value)
    {
        Debug.Log($" <b>Actor:</b> <b>{Name}</b> loses {value} mana");
        Mana -= value;
    }
}