using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class Card : ScriptableObject
{
    // Variables
    [SerializeField] private string ID = "";
    [SerializeField, TextArea(3, 5)] private string description;

    [SerializeField, HideInInspector] private CardEffect[] effects = new CardEffect[0];

    // Executes all effects
    public void Execute(Game game, Actor me, Actor target)
    {
        foreach (var effect in effects) {
            effect.CardBehaviour.Execute(game, me, target, effect.Data);
        }
    }

    private void OnEnable()
    {
        // Sets new GUID if none is set
        if(ID == "") {
            ID = Guid.NewGuid().ToString();
        }
    }

    [System.Serializable]
    public struct CardEffect
    {
        // Editable variables
        [SerializeField] private CardBehaviour cardBehaviour;
        [SerializeField] private CardData data;

        // Accessors
        public CardBehaviour CardBehaviour => cardBehaviour;
        public CardData Data => data;
         
        public CardEffect(CardBehaviour cardBehaviour, CardData data)
        {
            this.cardBehaviour = cardBehaviour ?? throw new ArgumentNullException(nameof(cardBehaviour));
            this.data = data;
        }
    }
}


// All types of data that can be shown
[Flags]
public enum DataTypes {
    Strenght = 1,
    Cost = 2 }

[Serializable]
public struct CardData
{
    [SerializeField] private int strenght;
    [SerializeField] private int cost;

    public int Strenght => strenght;
    public int Cost => cost;

    public CardData(int strenght, int cost)
    {
        this.strenght = strenght;
        this.cost = cost;
    }

    // Draws all properties according to mask
    public static void DrawProperties(float x, float y, float width, DataTypes types, SerializedProperty property)
    {
        var rect = new Rect(x, y, width, 15);

        var propertyMap = new Dictionary<DataTypes, string>() {
            { DataTypes.Strenght, "strenght" },
            { DataTypes.Cost, "cost" }
        };
        foreach (var p in propertyMap) {
            if (types.HasFlag(p.Key)) {
                EditorGUI.PropertyField(rect, property.FindPropertyRelative(p.Value));
                rect.y += 16;
            }
        }
    }
}

