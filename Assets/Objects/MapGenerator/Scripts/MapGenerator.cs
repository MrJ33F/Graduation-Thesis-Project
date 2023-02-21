using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Main Configurations")]
    public MapBiomeGenerator[] Biomes;
    public GameObject tilePrefab;


    [Header("Map Dimensions")]
    public int mapWidth = 50;
    public int mapHeight = 50;
    public float mapScale = 1.0f;
    public Vector2 mapOffset;

    [Header("Maps")]
    public MapConfigs[] mapConfigs;

    /// <summary> 
    /// Structura de date pentru configuratiile hartii
    /// [0] - HeightMap
    /// [1] - MoistureMap
    /// [2] - HeatMap
    ///</summary>
    [System.Serializable]
    public struct MapConfigs{
        public NoiseWave[] noiseWaves;
        public float[,] generatedMap;
    }

    private void Start() {
        GenerateMap();
    }   

    void GenerateMap(){
        // Height Map
        mapConfigs[0].generatedMap = NoiseGenerator.GenerateNoiseMap(mapWidth, mapHeight, mapScale, mapConfigs[0].noiseWaves, mapOffset);
    
        //Moisture Map
        mapConfigs[1].generatedMap = NoiseGenerator.GenerateNoiseMap(mapWidth, mapHeight, mapScale, mapConfigs[1].noiseWaves, mapOffset);

        //Heat Map
        mapConfigs[2].generatedMap = NoiseGenerator.GenerateNoiseMap(mapWidth, mapHeight, mapScale, mapConfigs[2].noiseWaves, mapOffset);

        for(int x = 0; x < mapWidth; x++){
            for(int y = 0; y < mapHeight; y++){
                GameObject mapTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                mapTile.GetComponent<SpriteRenderer>().sprite = GetBiome(mapConfigs[0].generatedMap[x, y], mapConfigs[1].generatedMap[x, y], mapConfigs[2].generatedMap[x, y]).GetRandomTileSprite();
            }
        }
    }

    MapBiomeGenerator GetBiome(float heightPoint, float moisturePoint, float heatPoint) {
        List<BiomeData> BiomeData = new List<BiomeData>();


        foreach(MapBiomeGenerator Biome in Biomes){
            if(Biome.BiomeMatchCondition(heightPoint, moisturePoint, heatPoint)){
                BiomeData.Add(new BiomeData(Biome));
            }
        }

        float currentValue = 0.0f;
        MapBiomeGenerator BiomeOutput = null;

        foreach(BiomeData bData in BiomeData){
            if(BiomeOutput == null){

                BiomeOutput = bData.Biome;
                currentValue = bData.GetDifferenceValue(heightPoint, moisturePoint, heatPoint);
            }
            else{
                if(bData.GetDifferenceValue(heightPoint, moisturePoint, heatPoint) < currentValue){
                    BiomeOutput = bData.Biome;
                    currentValue = bData.GetDifferenceValue(heightPoint, moisturePoint, heatPoint);

                }
            }
        }
        if(BiomeOutput == null) BiomeOutput = Biomes[0];

        return BiomeOutput;
    }
}

public class BiomeData{
    public MapBiomeGenerator Biome;

    public BiomeData(MapBiomeGenerator preset){
        Biome = preset;
    }

    public float GetDifferenceValue(float heightPoint, float moisturePoint, float heatPoint) {
        return (heightPoint - Biome.minimumHeight) +
               (moisturePoint - Biome.minimumMoisture) +
               (heatPoint - Biome.minimumHeat);
    }
}
