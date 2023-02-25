using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.Graphical
{
    public class ObjectsGenerator
    {
        private Transform objectParent;
        private readonly ISpaceOrientation spaceOrientation;

        public ObjectsGenerator(ISpaceOrientation spaceOrientation)
        {
            this.spaceOrientation = spaceOrientation;
        }

        public void Render(Transform parentTransform, List<BaseModels.AwaitingObject> awaitingObjects)
        {
            objectParent = CreateTransform("Object Parent", parentTransform);

            DrawObjects(awaitingObjects);
        }

        private Transform CreateTransform(string name, Transform parent)
        {
            GameObject gameObject = new GameObject(name);
            gameObject.transform.parent = parent;

            return gameObject.transform;
        }

        private void DrawObjects(List<BaseModels.AwaitingObject> awaitingObjects)
        {
            foreach (BaseModels.AwaitingObject ao in awaitingObjects)
            {
                Vector3 position = spaceOrientation.GetPositionVector(ao.Position);
                CreateObject(ao.AbstractObject as GameObject, position, ao.Scale);
            }
        }

        private GameObject CreateObject(GameObject prefab, Vector3 position, float scale)
        {
            GameObject gameObject = GameObject.Instantiate(prefab, position, Quaternion.identity, objectParent);
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameObject.transform.Rotate(spaceOrientation.GetObjectRotationVector());

            return gameObject;
        }
    }
}
