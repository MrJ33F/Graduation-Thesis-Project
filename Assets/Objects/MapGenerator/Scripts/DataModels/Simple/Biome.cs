using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Generator/Biome Data")]
public class Biome : ScriptableObject, IDataModelValidation
{
    public Sprite biomeGroundSprite;

    [Range(0, 100)];
    public float treesIntensity;
    //public List<TreeModeWithPriority> trees;

    [Range(0, 100)]
}
