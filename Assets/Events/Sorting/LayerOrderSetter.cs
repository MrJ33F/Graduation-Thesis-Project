using UnityEngine;
//using MapGenerator;

public class LayerOrderSetter : MonoBehaviour{
    public SpriteRenderer spriteRenderer;
    //private MapGenerator mapGenerator;

    private void Start() {
        MapGenerator = FindObjectOfType<MapGenerator<MapGenerator>();
        spriteRenderer.sortingOrder = (int)(MapGenerator.height - transform.position.z);
    }
}