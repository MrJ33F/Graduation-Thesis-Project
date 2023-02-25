using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapGenerator.Graphical
{
    public class SpaceOrientation3D : ISpaceOrientation
    {
        public Vector3 GetPositionVector(BaseModels.Vector2Float vector)
        {
            return new Vector3(vector.X, 0, vector.Y);
        }

        public Vector3 GetSpriteRotationVector()
        {
            return new Vector3(90, 0, 0);
        }

        public Vector3 GetObjectRotationVector()
        {
            return new Vector3();
        }

        public Tilemap.Orientation GetTilemapOrientation()
        {
            return Tilemap.Orientation.ZX;
        }

        public GridLayout.CellSwizzle GetGridOrientation()
        {
            return GridLayout.CellSwizzle.XZY;
        }
    }
}
