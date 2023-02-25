using System.Collections.Generic;
using UnityEngine;


namespace MapGenerator.Models{
    [CreateAssetMenu(menuName = "DataModels/Big Object")]
    public class BigObjectModel : PrefabModel, ModelValidator.IDataModelValidation{
        public int tileWidthCount = 1;
        public int tileHeightCount = 1;

        public BaseModels.BigObjectModel ToModel(){
            return new BaseModels.BigObjectModel {
                AbstractObject = prefab,
                TileHeightCount = tileHeightCount,
                TileWidthCount = tileWidthCount
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate(){
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateProperty(prefab, "Prefab");

            return dataModelValidator.Errors;
        }
    }
}