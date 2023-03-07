using UnityEditor;
using UnityEngine;

using MapGenerator.Models;

namespace MapGenerator.UnityPort
{
    [CustomPropertyDrawer(typeof(WaterNoiseMapParameters))]
    public class WaterNoiseMapParametersDrawer : PropertyDrawer
    {
        const int rowCount = 4;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int line = property.isExpanded ? (1+ rowCount) : 1;
            return EditorGUIUtility.singleLineHeight * line + ((line - 1) * 2);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            if (property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true))
            {
                EditorGUI.LabelField(position, label);
                EditorGUI.indentLevel++;

                var rowsPositions = CalculateRowPositions(position);

                EditorGUI.PropertyField(rowsPositions[0], property.FindPropertyRelative("octaves"),
                    new GUIContent("Octave", "Numarul de octave care vor afecta cat de detaliata for fi harta, valorile fiind adresate pe noise map."));
                EditorGUI.PropertyField(rowsPositions[1], property.FindPropertyRelative("frequency"),
                    new GUIContent("Frecventa", "Valoarea frecventei afecteaza numarul de spatii generate pe harta."));
                EditorGUI.PropertyField(rowsPositions[2], property.FindPropertyRelative("targetValue"),
                    new GUIContent("Valoarea tinta", "Valoarea asteptata din spectrumul de valori disponibile."));

                float min = property.FindPropertyRelative("minWaterPercent").floatValue;
                float max = property.FindPropertyRelative("maxWaterPercent").floatValue;
                EditorGUI.MinMaxSlider(rowsPositions[3], new GUIContent("Procentajul zonei acvatice",
                    "Zona in care se vor genera blocuri de tip acvatice bazata pe intervalul dat."), ref min, ref max, 0, 1);
                property.FindPropertyRelative("minWaterPercent").floatValue = min;
                property.FindPropertyRelative("maxWaterPercent").floatValue = max;

                EditorGUI.indentLevel--;
                EditorGUI.EndProperty();
            }
        }

        private Rect[] CalculateRowPositions(Rect position)
        {
            var rowsPositions = new Rect[rowCount];
            for (int i = 0; i < rowCount; ++i)
            {
                rowsPositions[i] = new Rect(position.x, position.y + (i + 1) * 18, position.width, 16);
            }
            return rowsPositions;
        }
    }
}
