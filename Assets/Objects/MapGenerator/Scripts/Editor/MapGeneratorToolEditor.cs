using UnityEngine;
using UnityEditor;

using MapGenerator.Generator;
using MapGenerator.Models;
using MapGenerator.Graphical;

namespace MapGenerator
{
    [CustomEditor(typeof(MapGeneratorTool))]
    public class MapGeneratorToolEditor : Editor
    {
        private SerializedProperty heightNoiseMapParameters;
        private SerializedProperty temperatureNoiseMapParameters;
        private SerializedProperty waterNoiseMapParameters;
        private SerializedProperty waterLayersProp;

        private MapGeneratorTool mapGenerator;

        void OnEnable()
        {
            waterNoiseMapParameters = serializedObject.FindProperty("waterNoiseMapParameters");
            heightNoiseMapParameters = serializedObject.FindProperty("heightNoiseMapParameters");
            temperatureNoiseMapParameters = serializedObject.FindProperty("temperatureNoiseMapParameters");
            waterLayersProp = serializedObject.FindProperty("waterLayers").FindPropertyRelative("waterBiomes");

            mapGenerator = (MapGeneratorTool)target;
        }

        public override void OnInspectorGUI()
        {
            DrawSizeSection();
            EditorGUILayout.Space();

            DrawNoiseMapsParametersSection();
            EditorGUILayout.Space();

            DrawWaterBiomesSection(mapGenerator.waterLayers);
            EditorGUILayout.Space();

            DrawBiomesDiagramSection(mapGenerator.biomesDiagram);
            EditorGUILayout.Space();

            DrawGenerationSection();
            EditorGUILayout.Space();

            DrawButtonSection();
        }

        private void DrawSizeSection()
        {
            EditorGUILayout.LabelField("Size", EditorStyles.boldLabel);
            mapGenerator.width = EditorGUILayout.IntSlider(new GUIContent("Latimea", "Numarul de tile-uri pe axa X"),
                                                           mapGenerator.width, 10, 300);
            mapGenerator.height = EditorGUILayout.IntSlider(new GUIContent("Inaltimea", "Numarul de tile-uri pe axa Y"),
                                                            mapGenerator.height, 10, 300);         
        }

        private void DrawNoiseMapsParametersSection()
        {
            EditorGUILayout.LabelField("Noise Maps Parameters", EditorStyles.boldLabel);
            serializedObject.Update();
            EditorGUILayout.PropertyField(heightNoiseMapParameters, new GUIContent("Parametrii harta de inaltime."), true);
            EditorGUILayout.PropertyField(temperatureNoiseMapParameters, new GUIContent("Parametri harta de temperatura."), true);
            EditorGUILayout.PropertyField(waterNoiseMapParameters, new GUIContent("Parametri harta acvatica."), true);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawWaterBiomesSection(WaterLayers waterLayers)
        {
            EditorGUILayout.LabelField("Water Biomes", EditorStyles.boldLabel);

            serializedObject.Update();
            for (int i=0; i< waterLayers.waterBiomes.Count; ++i)
            {
                EditorGUILayout.PropertyField(waterLayersProp.GetArrayElementAtIndex(i), new GUIContent($"Nivelul apei {i}"), true);
            }

            if (GUILayout.Button("Adauga nivel apa", GUILayout.Width(150)))
                waterLayers.AddWaterLevel();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawBiomesDiagramSection(BiomesDiagram biomesDiagram)
        {
            EditorGUILayout.LabelField("Biomes Diagram", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            biomesDiagram.heightLayerCount = EditorGUILayout.IntField(
                new GUIContent("Numarul straturilor de inaltime", "Numarul de straturi de inaltimi din diagrama de biom-uri."),
                biomesDiagram.heightLayerCount, GUILayout.MinWidth(10));
            biomesDiagram.temperatureLayerCount = EditorGUILayout.IntField(
                new GUIContent("Numarul straturilor de temperatura", "Numarul de straturi de temperaturi din diagrama de biom-uri."),
                biomesDiagram.temperatureLayerCount, GUILayout.MinWidth(10));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            for (int j = -1; j < biomesDiagram.temperatureLayerCount; j++)
            {
                EditorGUILayout.BeginVertical();
                for (int i = 0; i <= biomesDiagram.heightLayerCount; i++)
                {
                    if (j == -1 && i == biomesDiagram.heightLayerCount)
                        GUILayout.Label("");
                    else if (j == -1)
                        GUILayout.Label($"Inaltime {biomesDiagram.heightLayerCount - i}");
                    else if (i == biomesDiagram.heightLayerCount)
                        GUILayout.Label($"Temperatura {j + 1}");
                    else
                        biomesDiagram[i,j] = (Biome)EditorGUILayout.ObjectField(biomesDiagram[i, j], typeof(Biome), true);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawGenerationSection()
        {
            EditorGUILayout.LabelField(new GUIContent("Generare"), EditorStyles.boldLabel);

            mapGenerator.generationType = (GraphicalGenerationType)EditorGUILayout.EnumPopup(
                new GUIContent("Tipul de generare", "The way a graphic representation of the map will be generated."),
                mapGenerator.generationType);

            mapGenerator.orientationType = (SpaceOrientationType)EditorGUILayout.EnumPopup(
                new GUIContent("Orentaria spatiala", "Orientarea spatiala la care harta va fi generata."),
                mapGenerator.orientationType);

            mapGenerator.generateOnStart = EditorGUILayout.Toggle(
                new GUIContent("Genereaza la start.", "Sa fie generata harta in momentul cand se activeaza scena?"),
                mapGenerator.generateOnStart);

            mapGenerator.generateRandomSeed = EditorGUILayout.Toggle(
                new GUIContent("Genereaza seed aleatoriu.", "Sa fie generata harta folosind un seed aleatoriu?"),
                mapGenerator.generateRandomSeed);

            if (!mapGenerator.generateRandomSeed)
            {
                EditorGUILayout.BeginHorizontal();

                mapGenerator.seed = EditorGUILayout.IntField(
                    new GUIContent("Seed", "Factor aleatoriu, care este folosit la generarea unei harti."),
                    mapGenerator.seed);

                if (GUILayout.Button("Seed Aleatoriu"))
                    mapGenerator.RandomSeed();

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawButtonSection()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Generate"))
            {
                if (mapGenerator.generateRandomSeed)
                    mapGenerator.RandomSeed();
            
                mapGenerator.TryGenerate();
            }

            if (GUILayout.Button("Curata"))
            {
                mapGenerator.Clear();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}