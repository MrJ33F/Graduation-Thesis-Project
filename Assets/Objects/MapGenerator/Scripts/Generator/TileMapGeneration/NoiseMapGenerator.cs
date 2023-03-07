using System;
using MapGenerator.BaseModels;

namespace MapGenerator.Generator{
    public class NoiseMapGenerator{
        private float xOff, yOff;

        private Random random;

        public NoiseMapGenerator(Random random){
            this.random = random;
        }

        public float[,] Generate(NoiseMapParametersModel noiseMapParameters, int width, int height){
            xOff = random.Next(0, 1000000);
            yOff = random.Next(0, 1000000);

            float[,] noiseMap = new float[width, height];

            for(int i = 0; i < height; ++i){
                for(int j = 0; j < width; ++j){
                    noiseMap[i, j] = CalculateValue(noiseMapParameters, j, i);
                }
            }

            return noiseMap;
        }

        private float CalculateValue(NoiseMapParametersModel noiseMapParameters, int x, int y){
            float noise = 0.0f;
            float gain = 1.0f;

            for(int i = 0; i < noiseMapParameters.Octaves; ++i){
                float value = ImprovedNoise.Noise2D((x + xOff) * gain / noiseMapParameters.Frequency,
                                                    (y + yOff) * gain / noiseMapParameters.Frequency);
                noise += value * 0.5f / gain;
                gain *= 2;
            }

            return (float)Math.Pow(noise, noiseMapParameters.TargetValue);
        }
    }
}