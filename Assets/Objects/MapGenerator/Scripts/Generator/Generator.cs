using System;
using System.Collections.Generic;

namespace MapGenerator.Generator{
    public class Generator{
        public List<BaseModels.AwaitingObject> AwaitingObjects {get; private set;}
        public BaseModels.TilesMap Map {get; set;}

        private readonly BaseModels.WaterBiomeModel[] waterBiomes;
        private readonly BaseModels.BiomeModel[,] biomes;

        private readonly BaseModels.WaterNoiseMapParametersModel waterNoiseMapParameters;
        private readonly BaseModels.GroundNoiseMapParametersModel heightNoiseMapParameters;
        private readonly BaseModels.GroundNoiseMapParametersModel temperatureNoiseMapParameters; 
        
        private readonly Random random;

        public Generator(int width, int height, int seed,
                         BaseModels.WaterBiomeModel[] waterBiomes,
                         BaseModels.BiomeModel[,] biomes,
                         BaseModels.WaterNoiseMapParametersModel waterNoiseMapParameters,
                         BaseModels.GroundNoiseMapParametersModel heightNoiseMapParameters,
                         BaseModels.GroundNoiseMapParametersModel temperatureNoiseMapParameters)
        {
            this.biomes = biomes;
            this.waterBiomes = waterBiomes;
            this.waterNoiseMapParameters = waterNoiseMapParameters;
            this.heightNoiseMapParameters = heightNoiseMapParameters;
            this.temperatureNoiseMapParameters = temperatureNoiseMapParameters;

            random = new Random(seed);
            Map = new BaseModels.TilesMap(width, height);
        }

        public void Generate()
        {
            BiomeMapGenerator biomMapGenerator = new BiomeMapGenerator(random, biomes, temperatureNoiseMapParameters, heightNoiseMapParameters);
            biomMapGenerator.GenerateBiomMap(Map);

            BiomeMapSmoother biomMapSmoother = new BiomeMapSmoother();
            biomMapSmoother.SmoothBiomeMap(Map);

            WaterMapGenerator waterMapGenerator = new WaterMapGenerator(random, waterBiomes, waterNoiseMapParameters);
            waterMapGenerator.GenerateWaterMap(Map);

            WaterMapSmoother waterMapSmoother = new WaterMapSmoother(waterBiomes);
            waterMapSmoother.SmoothWaterMap(Map);

            ObjectGenerator objectGenerator = new ObjectGenerator(Map, random);
            AwaitingObjects = objectGenerator.Generate();
        }
    }
}