using System;
using System.Collections.Generic;

[Serializable]
public class BiomesDiagramContainer : IDataModelValidation{
    public const int MAX_X_LAYER_COUNT = 6;
    public const int MAX_Y_LAYER_COUNT = 6;

    public int temperatureLayerCount = 2;
    public int heightLayerCount = 2;

    public Biome[] biomes = new Biome[MAX_X_LAYER_COUNT * MAX_Y_LAYER_COUNT];

    public Biome this[int i, int j]
    {
        get{return biomes[i * MAX_Y_LAYER_COUNT + j];}
        set{biomes[i*MAX_Y_LAYER_COUNT + j] = value;}
    }

    public DataModels.BiomeModel[,] ToModel(){
        
    }

}