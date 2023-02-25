using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.Models
{
    [CreateAssetMenu(menuName = "Map Generator/Tree Data")]
    public class TreeModel : PrefabModel, ModelValidator.IDataModelValidation
    {
        public float minScale = 0.8f;
        public float maxScale = 1.5f;

        public BaseModels.TreeModel ToModel()
        {
            return new BaseModels.TreeModel()
            {
                AbstractObject = prefab,
                MinScale = minScale,
                MaxScale = maxScale
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateProperty(prefab, "Prefab");

            return dataModelValidator.Errors;
        }
    }
}