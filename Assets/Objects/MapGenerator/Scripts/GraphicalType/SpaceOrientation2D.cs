using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapGenerator.Graphical
{
    public class SpaceOrientation2D : ISpaceOrientation
    {
        public Vector3 GetPositionVector(BaseModels.Vector2Float vector)
        {
            return new Vector3(vector.X, vector.Y, 0);
        }

        public Vector3 GetSpriteRotationVector()
        {
            return new Vector3();
        }

        public Vector3 GetObjectRotationVector()
        {
            return new Vector3(-90, 0, 0);
        }

        public Tilemap.Orientation GetTilemapOrientation()
        {
            return Tilemap.Orientation.XY;
        }

        public GridLayout.CellSwizzle GetGridOrientation()
        {
            return GridLayout.CellSwizzle.XYZ;
        }
    }
}
