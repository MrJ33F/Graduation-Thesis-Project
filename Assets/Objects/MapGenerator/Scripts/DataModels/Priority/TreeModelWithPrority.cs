using System;
using System.Collections.Generic;

namespace MapGenerator.Models
{
    [Serializable]
    public class TreeModelWithPriority : ModelWithPriority<TreeModel>, ModelValidator.IDataModelValidation
    {
        public BaseModels.PriorityModel<BaseModels.TreeModel> ToModel()
        {
            return new BaseModels.PriorityModel<BaseModels.TreeModel>()
            {
                Priority = priority,
                Model = model.ToModel()
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