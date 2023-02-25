using UnityEngine;

namespace MapGenerator.Graphical
{
    public interface IGraphicalMapGenerator
    {
        void Render(Transform parentTransform, BaseModels.TilesMap map);
    }
}
