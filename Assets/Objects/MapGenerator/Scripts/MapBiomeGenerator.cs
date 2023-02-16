using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Biome Preset")]
public class MapBiomeGenerator : ScriptableObject
{
    public Sprite[] tileArray;

    public float minimumHeight;
    public float minimumMoisture;
    public float minimumHeat;

    public Sprite GetRandomTileSprite(){
        return tileArray[Random.Range(0, tileArray.Length)];
    }

    public bool BiomeMatchCondition(float height, float moisture, float heat){
        return height >= minimumHeight && 
               moisture >= minimumMoisture && 
               heat >= minimumHeat;
    }
}
