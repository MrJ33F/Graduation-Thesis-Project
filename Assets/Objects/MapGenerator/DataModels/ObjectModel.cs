using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.Models
{
    [CreateAssetMenu(menuName = "Map Generator/Object")]
    public class ObjectModel : PrefabModel, ModelValidator.IDataModelValidation
    {
        public BaseModels.ObjectModel ToModel()
        {
            return new BaseModels.ObjectModel()
            {
                AbstractObject = prefab
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