using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Card : ScriptableObject
{
    [SerializeField] private string ID = "";
    [SerializeField, TextArea(3, 5)] private string description;

    [SerializeField] private CardEffect[] effects = new CardEffect[0];

    public void Execute(Game game, Actor me, Actor target)
    {
        foreach (var effect in effects) {
            effect.CardBehaviour.Execute(game, me, target, effect.Data);
        }
    }

    private void OnEnable()
    {
        if(ID == "") {
            ID = Guid.NewGuid().ToString();
        }
    }

    [System.Serializable]
    public struct CardEffect
    {
        [SerializeField] private CardBehaviour cardBehaviour;
        [SerializeField] private CardData data;

        public CardBehaviour CardBehaviour => cardBehaviour;
        public CardData Data => data;

        public CardEffect(CardBehaviour cardBehaviour, CardData data)
        {
            this.cardBehaviour = cardBehaviour ?? throw new ArgumentNullException(nameof(cardBehaviour));
            this.data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}


[Flags]
public enum DataTypes { Strenght, Cost }

[Serializable]
public class CardData
{
    [SerializeField] private int strenght;
    [SerializeField] private int cost;

    public int Strenght => strenght;
    public int Cost => cost;
}

