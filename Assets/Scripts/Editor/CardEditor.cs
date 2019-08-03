using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    ReorderableList effectsList;

    private void OnEnable()
    {
        var effects = serializedObject.FindProperty("effects");
        // Creates list and adds callbacks
        effectsList = new ReorderableList(serializedObject, effects, true, true, true, true) {
            // Draws label on top
            drawHeaderCallback = rect => {
                GUI.Label(rect, "Effects");
            },

            // Draws given element
            drawElementCallback = (rect, i, active, focused) => DrawElement(effects, rect, i, active, focused),

            // Gets height of given element
            elementHeightCallback = i => {
                var element = effects.GetArrayElementAtIndex(i);
                CardBehaviour behavaiour = (CardBehaviour)element.FindPropertyRelative("cardBehaviour").objectReferenceValue;

                float height = 16 + 5 + 10;
                if (behavaiour != null) {
                    height += 16 * CountBits((int)behavaiour.DataMask);
                }
                return height;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        // No idea what it is fopr exactly
        serializedObject.Update();

        // Draws default inspector, and reordeable list
        base.OnInspectorGUI();
        GUILayout.Space(16);
        effectsList.DoLayoutList();

        // Apllies changes with undo
        serializedObject.ApplyModifiedProperties();
    }

    public void DrawElement(SerializedProperty effects, Rect rect, int i, bool active, bool focused)
    {
        var element = effects.GetArrayElementAtIndex(i);
        CardBehaviour behaviour = (CardBehaviour)element.FindPropertyRelative("cardBehaviour").objectReferenceValue;

        rect.y += 4;

        var labelRect = new Rect(rect.x, rect.y, rect.width, 16);

        // Draws popup of all SOs
        DrawLabelPopup(element, labelRect);

        EditorGUI.indentLevel += 1;

        // Draws variables if behaviour is not null
        if (behaviour != null) {
            // Draws all variables 5 pixels below label
            var propertyRect = new Rect(rect.x, rect.y + 16 + 5, rect.width, 16 * CountBits((int)behaviour.DataMask));
            CardData.DrawProperties(propertyRect.x, propertyRect.y, propertyRect.width, behaviour.DataMask, element.FindPropertyRelative("data"));
        }

        EditorGUI.indentLevel -= 1;
    }

    private static void DrawLabelPopup(SerializedProperty element, Rect labelRect)
    {
        // Gets list of SOs in assets 
        CardBehaviour[] allBehaviours = AssetDatabase.FindAssets($"t:{nameof(CardBehaviour)}")
            .Select(s => AssetDatabase.GUIDToAssetPath(s))
            .Select(s => (CardBehaviour)AssetDatabase.LoadAssetAtPath(s, typeof(CardBehaviour)))
            .ToArray();

        // Gets current behaviour
        var behaviourProperty = element.FindPropertyRelative("cardBehaviour");
        var currentBehaviour = behaviourProperty.objectReferenceValue;

        // gets index of current behaviour
        int index = -1;
        for (int j = 0; j < allBehaviours.Length; j++) {
            if (allBehaviours[j] == currentBehaviour) {
                index = j;
                break;
            }
        }
        
        // Gets all options
        string[] options = allBehaviours.Select(b => b.name).ToArray();

        // Draws popup
        int result = EditorGUI.Popup(labelRect, index == -1 ? "[select type]" : options[index], index, options);
        behaviourProperty.objectReferenceValue = result == -1 ? null : allBehaviours[result];
    }

    // Counts bits in int, used for enum flags
    public int CountBits(int i)
    {
        i = i - ((i >> 1) & 0x55555555);
        i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
        return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
    }
}
