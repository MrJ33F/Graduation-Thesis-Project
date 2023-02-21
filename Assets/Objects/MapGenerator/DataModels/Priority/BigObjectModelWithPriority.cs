using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.Models{
    [Serializable]
    public class BigObjectModelWithPriority : ModelWithPriority<BigObjectModel>, ModelValidator.IDataModelValidation
    {
        [Range(1, 100)]
        public int maxCount = 100;

        public BaseModels.PriorityModel<BaseModels.BigObjectModel> ToModel()
        {
            return new BaseModels.PriorityModel<BaseModels.BigObjectModel>()
            {
                Model = model.ToModel(),
                Priority = priority,
                MaxCount = maxCount
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateModel(model, "Model");

            return dataModelValidator.Errors;
        }
    }
}