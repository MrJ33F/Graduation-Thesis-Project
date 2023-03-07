using System;
using System.Linq;
using System.Collections.Generic;

using MapGenerator.BaseModels;

namespace MapGenerator.Generator{
    public class WaterMapGenerator
    {
        private class WaterTile{
            public float DeepnessValue { get; set; }
            public Vector2Int Position { get; set; }

            public WaterTile(float deepnessValue, Vector2Int position){
                DeepnessValue = deepnessValue;
                Position = position;
            }
        }

        private readonly WaterBiomeModel[] waterBiomes;
        private readonly WaterNoiseMapParametersModel waterNoiseMapParameters;

        private NoiseMapGenerator noiseGenerator;

        public WaterMapGenerator(Random random,
                                 WaterBiomeModel[] waterBiomes,
                                 WaterNoiseMapParametersModel waterNoiseMapParameters){
                                    this.waterBiomes = waterBiomes;
                                    this.waterNoiseMapParameters = waterNoiseMapParameters;
                                    this.noiseGenerator = new NoiseMapGenerator(random);
                                 }

        public void GenerateWaterMap(TilesMap map){
            if(waterBiomes.Length > 0){
                float threshold = waterBiomes[0].WaterThresholding;

                float[,] waterDeepnessMap = noiseGenerator.Generate(waterNoiseMapParameters, map.Width, map.Height);
                List<WaterTile> waterTiles = GetAllWaterTiles(waterDeepnessMap, threshold, out int deepEnoughTilesCount);
                SetWaterMap(map, waterTiles, deepEnoughTilesCount, threshold);
            }
        }

        private List<WaterTile> GetAllWaterTiles(float[,] waterDeapnesMap, float firstThreshold, out int deepEnoughTilesCount)
        {
            deepEnoughTilesCount = 0;
            List<WaterTile> waterPoints = new List<WaterTile>();
            for (int i = 0; i < waterDeapnesMap.GetLength(0); i++)
            {
                for (int j = 0; j < waterDeapnesMap.GetLength(1); j++)
                {
                    waterPoints.Add(new WaterTile(waterDeapnesMap[i, j], new Vector2Int(j, i)));
                    if (waterDeapnesMap[i, j] >= firstThreshold)
                        ++deepEnoughTilesCount;
                }
            }

            return waterPoints.OrderByDescending(x => 
                                                 x.DeepnessValue).ToList();
        }

        private void SetWaterMap(TilesMap map, List<WaterTile> waterTiles, int deepEnoughTilesCount, float threshold){
            int waterTilesCount = CalculateWaterTilesCount(deepEnoughTilesCount, waterTiles.Count);

            if(waterTilesCount == 0) return;

            WaterTile shallowestWaterTile = waterTiles[waterTilesCount - 1];
            float valueOffset = -shallowestWaterTile.DeepnessValue + threshold;

            for(int i = 0; i < waterTilesCount; i++){
                WaterTile waterTile = waterTiles[i];
                Vector2Int position = waterTile.Position;
                SetWaterTile(map[position.Y, position.X], waterTile.DeepnessValue + valueOffset);
            }
        }

        private int CalculateWaterTilesCount(int deepEnoughTilesCount, int totalTilesCount)
        {
            if (deepEnoughTilesCount < (waterNoiseMapParameters.MinWaterPercent * totalTilesCount))
                return (int)(waterNoiseMapParameters.MinWaterPercent * totalTilesCount);
            else if (deepEnoughTilesCount > (waterNoiseMapParameters.MaxWaterPercent * totalTilesCount))
                return (int)(waterNoiseMapParameters.MaxWaterPercent * totalTilesCount);
            else
                return deepEnoughTilesCount;
        }

        private BiomeModel CalculateWaterBiome(float value){
            BiomeModel biome = null;

            for(int i = 0; i < waterBiomes.Length; i++){
                if(waterBiomes[i].WaterThresholding <= value) biome = waterBiomes[i].Biome;
                else break;
            }

            return biome;
        }

        private void SetWaterTile(Tile tile, float value){
            tile.WaterBiome = CalculateWaterBiome(value);
            tile.WaterDeepness = value;
        }
    }
}
