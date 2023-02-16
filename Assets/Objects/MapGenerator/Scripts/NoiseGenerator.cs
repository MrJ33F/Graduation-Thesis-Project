using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static float[,] GenerateNoiseMap(int width, int height, float scale, NoiseWave[] noiseWaves, Vector2 noiseOffset)
{
    // cream vectorul de noise map
    float[,] noiseMap = new float[width, height];
    

    for(int x = 0; x < width; ++x)
    {
        for(int y = 0; y < height; ++y)
        {
            // calculam cate o secventa 
            float noiseSamplePosX = (float)x * scale + noiseOffset.x;
            float noiseSamplePosY = (float)y * scale + noiseOffset.y;

            float normalization = 0.0f;

            // mergem prin fiecare wave
            foreach(NoiseWave noiseWave in noiseWaves)
            {
                // facem monstre ale zgomotului perlin luand in considerare amplitudinea si frecventa
                noiseMap[x, y] += noiseWave.amplitude * Mathf.PerlinNoise(
                    noiseSamplePosX * noiseWave.frequency + noiseWave.seed,
                    noiseSamplePosY * noiseWave.frequency + noiseWave.seed
                );
                normalization += noiseWave.amplitude;
            }
            // normalizam la final valoarea
            noiseMap[x, y] /= normalization;
        }
    }
        
    return noiseMap;
}

}

[System.Serializable]
public class NoiseWave{
    public float seed;
    public float frequency;
    public float amplitude;
}
