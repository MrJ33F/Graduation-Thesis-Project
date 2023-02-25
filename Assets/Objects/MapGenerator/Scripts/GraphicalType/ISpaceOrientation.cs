using UnityEngine;

namespace MapGenerator.Graphical
{
    public interface ISpaceOrientation
    {
        Vector3 GetPositionVector(BaseModels.Vector2Float vector);
        Vector3 GetSpriteRotationVector();
        Vector3 GetObjectRotationVector();

        UnityEngine.Tilemaps.Tilemap.Orientation GetTilemapOrientation();
        GridLayout.CellSwizzle GetGridOrientation();
    }
}
