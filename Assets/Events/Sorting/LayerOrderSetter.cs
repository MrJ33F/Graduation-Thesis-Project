using UnityEngine;
using MapGenerator.Generator;

public class LayerOrderSetter : MonoBehaviour{
    public SpriteRenderer spriteRenderer;
    private MapGeneratorTool mapGeneratorTool;

    void Start()
    {
        mapGeneratorTool = FindObjectOfType<MapGeneratorTool>();
        spriteRenderer.sortingOrder = (int)(mapGeneratorTool.height - transform.position.z);
    }
}